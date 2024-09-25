using Microsoft.AspNetCore.Mvc;

namespace LogApplication.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private static readonly Random _random = new Random();

        [HttpGet("temperature")]
        public IActionResult GetRandomTemperature()
        {
            return Ok(_random.Next(-20, 40)); // Random temperature between -20 and 40
        }

        [HttpGet("humidity")]
        public IActionResult GetRandomHumidity()
        {
            return Ok(_random.Next(0, 100)); // Random humidity between 0 and 100%
        }

        [HttpGet("pressure")]
        public IActionResult GetRandomPressure()
        {
            return Ok(_random.Next(950, 1050)); // Random pressure between 950 and 1050 hPa
        }


    }
}
