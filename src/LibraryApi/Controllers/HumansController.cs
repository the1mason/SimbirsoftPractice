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

        [HttpGet]
        public async Task<ActionResult<List<Models.HumanDto>>> GetHumans(bool authorsOnly = false, string query = null, int index = 0, int? count = null)
        {
            if (index < 0)
                return BadRequest("Index can't be lower, than 0");
            if (count < 1)
                return BadRequest("Count is a positive number. It can't be lower, than 1");

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
