using Microsoft.AspNetCore.Mvc;
using WebApiWithControllers.Models;

namespace WebApiWithControllers.Controllers
{
    // Сгенерированный VS контроллер
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) {
            _logger = logger;
        }

        // сгенерированый метод 
        [HttpGet]
        public IEnumerable<WeatherForecast> Get() {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // написали сами
        [HttpGet]
        public string Hello() {
            return $"Привет, на сервере {DateTime.Now:dd-MM-yyyyy HH:mm:ss}";
        }
    }
}