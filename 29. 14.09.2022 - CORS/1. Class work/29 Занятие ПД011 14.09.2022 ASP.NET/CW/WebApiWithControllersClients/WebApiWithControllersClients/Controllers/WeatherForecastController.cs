using Microsoft.AspNetCore.Mvc;
using WebApiWithControllersClients.Models;

namespace WebApiWithControllersClients.Controllers
{
    //  од сгенерирован Visual Studio
    // ѕример контроллера API, которому не требуетс€ возвращать представлени€,
    // тогда достаточно наследоватьс€ от ControllerBase
    [ApiController]
    [Route("[controller]/{action}")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[] {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() {
            // имитаци€ получеи€ данных
            Thread.Sleep(800);

            return Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .ToArray();
        }
    }
}