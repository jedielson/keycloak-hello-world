using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("identity")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices] ILogger<WeatherForecastController> logger)
        {
            logger.LogInformation("Starting token lolo {date}", DateTime.Now);
            return Ok(from c in User.Claims select new {c.Type, c.Value});
        }
    }
}
