using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumansController : ControllerBase
    {
        private readonly ILogger<HumansController> _logger;
        public HumansController(ILogger<HumansController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 3.1 3.1.1 3.1.2 3.1.3 - GET request for Humans
        /// </summary>
        /// <param name="authorsOnly">should return only authors?</param>
        /// <param name="query">search query for name, surname and patronymic</param>
        /// <param name="index">start index</param>
        /// <param name="count">count of items returned</param>
        [HttpGet]
        public async Task<ActionResult<List<Models.HumanDto>>> GetHumans(bool authorsOnly = false, string query = null, int index = 0, int? count = null)
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
            catch(Exception ex)
            {
                _logger.LogError($"[{DateTime.UtcNow}] {HttpContext.Connection.RemoteIpAddress} : Inner exception {ex.Message} in {ex.StackTrace}");
                return StatusCode(500, "Server error: " + ex.Message);
            }
        }
    }
}
