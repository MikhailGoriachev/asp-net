using Microsoft.AspNetCore.Mvc;

namespace StarWarsUniverse.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    
}

