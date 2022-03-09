using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 4.1 4.1.1 4.1.2
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Models.BookDto>> GetBooks(int index = 0, int? count = null, int? authorId = null)
        {
            if (index < 0)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (index < 0)");
                return BadRequest("Index can't be lower, than 0");
            }
            if (count < 1)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (count < 1)");
                return BadRequest("Count is a positive number. It can't be lower, than 1");
            }

            try
            {
                List<Models.BookDto> result = Services.BookService.Get(index, count, authorId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} : Inner exception {ex.Message} in {ex.StackTrace}");
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        /// <summary>
        /// 4.2
        /// </summary>
        /// <param name="book"></param>
        [HttpPost]
        public ActionResult<Models.BookDto> AddBook(Models.BookDto book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (title is null)");
                return BadRequest("Title can't be null");
            }
            if (book.Genre == null)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (genre is null)");
                return BadRequest("Genre can't be null");
            }
            if (book.Genre.Name == null)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (genre name is null)");
                return BadRequest("Genre name can't be null");
            }
            try
            {
                if (!Data.Storage.Humans.Any(x => x.Id == book.AuthorId))
                {
                    _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (bad user id)");
                    return BadRequest("Bad user id");
                }

                
                Models.BookDto result = Services.BookService.Add(book);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} : Inner exception {ex.Message} in {ex.StackTrace}");
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        /// <summary>
        /// 4.3
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public ActionResult DeleteBook(int? id)
        {
            if (id == null)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (id is null)");
                return BadRequest("Id can't be null");
            }

            if (!Data.Storage.Books.Any(x => x.Id == id))
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (wrong id)");
                return BadRequest("Can't find book with id " + id);
            }

            try
            {
                Services.BookService.Delete(Convert.ToInt32(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} : Inner exception {ex.Message} in {ex.StackTrace}");
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }
    }
}
