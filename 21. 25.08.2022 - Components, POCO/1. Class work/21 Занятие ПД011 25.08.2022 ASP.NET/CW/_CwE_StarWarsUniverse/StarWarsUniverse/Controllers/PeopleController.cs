using Microsoft.AspNetCore.Mvc;

namespace StarWarsUniverse.Controllers;

public class PeopleController : Controller
{
    public IActionResult Index() => View();

}


