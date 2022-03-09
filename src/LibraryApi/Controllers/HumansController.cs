using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HumansController : ControllerBase
    {
        private readonly ILogger<HumansController> _logger;
        public HumansController(ILogger<HumansController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 1.3.1 1.3.1.1 1.3.1.2 1.3.1.3 - GET request for Humans
        /// </summary>
        /// <param name="authorsOnly">should return only authors?</param>
        /// <param name="query">search query for name, surname and patronymic</param>
        /// <param name="index">start index</param>
        /// <param name="count">count of items returned</param>
        [HttpGet]
        public ActionResult<List<Models.HumanDto>> GetHumans(bool authorsOnly = false, string query = null, int index = 0, int? count = null)
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
                List<Models.HumanDto> result = Services.HumanService.Get(index, count, authorsOnly, query);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} : Inner exception {ex.Message} in {ex.StackTrace}");
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        /// <summary>
        /// 1.3.2 - POST request for Humans
        /// </summary>
        /// <param name="human">new human to add</param>
        [HttpPost]
        public ActionResult<Models.HumanDto> AddHuman(Models.HumanDto human)
        {
            if (string.IsNullOrWhiteSpace(human.Name))
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (name is null)");
                return BadRequest("Name can't be null");
            }
            if (string.IsNullOrWhiteSpace(human.Surname))
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (surname is null)");
                return BadRequest("Surname can't be null");
            }
            try
            {
                Models.HumanDto result = Services.HumanService.Add(human);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} : Inner exception {ex.Message} in {ex.StackTrace}");
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }

        /// <summary>
        /// 1.3.3 - DELETE request for Humans
        /// </summary>
        /// <param name="id">id of user to delete</param>
        [HttpDelete]
        public ActionResult DeleteHuman(int? id)
        {
            if (id == null)
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (id is null)");
                return BadRequest("Id can't be null");
            }

            if (!Data.Storage.Humans.Any(x => x.Id == id))
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} -> 400 (wrong id)");
                return BadRequest("Can't find human with id " + id);
            }

            try
            {
                Services.HumanService.Delete(Convert.ToInt32(id));
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
