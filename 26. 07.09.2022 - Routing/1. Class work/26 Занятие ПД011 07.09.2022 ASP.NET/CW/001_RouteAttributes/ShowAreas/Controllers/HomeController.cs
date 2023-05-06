using Microsoft.AspNetCore.Mvc;

namespace AreasIntro.Controllers;

public class HomeController : Controller
{
   // localhost::pppp/Index
    public IActionResult Index() => View();

} // class HomeController
