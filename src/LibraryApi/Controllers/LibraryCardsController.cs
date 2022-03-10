using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LibraryApi.Controllers
{
    /// <summary>
    /// 2.1.2
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class LibraryCardsController : ControllerBase
    {
        private readonly ILogger<LibraryCardsController> _logger;
        public LibraryCardsController(ILogger<LibraryCardsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 2.1.4
        /// </summary>
        /// <param name="humanId"></param>
        /// <param name="bookId"></param>
        [HttpPost("{bookId}")]
        public ActionResult<Models.BookDto> GetBook([FromBody] int humanId, [FromRoute] int bookId)
        {
            try
            {
                Models.BookDto result = Services.CardService.Add(Convert.ToInt32(humanId), Convert.ToInt32(bookId));
                return result;
            }
            catch (Exceptions.BookTakenException ex)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (book taken)");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (bad id)");
                return BadRequest("Wrong human or book id");
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} : Inner exception {ex.Message} in {ex.StackTrace}");
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }
    }
}
