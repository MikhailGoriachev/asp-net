using Microsoft.AspNetCore.Mvc;

namespace AreasIntro.Areas.First.Controllers;

[Area("First")]
public class HomeController : Controller
{
    // каким маршрутам соответствует этот метод действия:
    // GET /First/Home/Index    
    // GET /First/Home/ 
    public IActionResult Index() => View();


    // атрибуты имеют высший приоритет по отношению к коду в Program.cs
    // каким маршрутам соответствует этот метод действия:
    // GET /First/Test    
    [Route("{action}")]
    public IActionResult Test() => View();

} // class HomeController

