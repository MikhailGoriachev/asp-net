using Microsoft.AspNetCore.Mvc;

namespace StarWarsUniverse.Controllers;
public class PlanetsController : Controller
{
    public IActionResult Index() => View();
}

