using Microsoft.AspNetCore.Mvc;
using WebApiWithControllers.Models;

namespace WebApiWithControllers.Controllers
{
    // ��������������� VS ����������
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

        // �������������� ����� 
        [HttpGet]
        public IEnumerable<WeatherForecast> Get() {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // �������� ����
        [HttpGet]
        public string Hello() {
            return $"������, �� ������� {DateTime.Now:dd-MM-yyyyy HH:mm:ss}";
        }
    }
}