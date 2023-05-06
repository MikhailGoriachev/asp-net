using Microsoft.AspNetCore.Mvc;

namespace TouristicAgencyMvcCore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
} // class HomeController

