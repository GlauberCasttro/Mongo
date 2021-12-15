using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Lista os itens de wheather
        /// </summary>
        /// <returns>Lista de wheather</returns>
        /// <response code="200"></response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var rng = new Random();

            var result = await Task.Run(() => Enumerable.Range(1, 5)
              .Select(index => new WeatherForecast
              {
                  Date = DateTime.Now.AddDays(index),
                  TemperatureC = rng.Next(-20, 55),
                  Summary = Summaries[rng.Next(Summaries.Length)]
              })
          .ToArray());

            return Ok(result);
        }
    }
}