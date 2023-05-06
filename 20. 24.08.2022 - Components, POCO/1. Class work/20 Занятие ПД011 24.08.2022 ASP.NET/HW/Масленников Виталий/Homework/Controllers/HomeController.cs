using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

public class HomeController : Controller
{
    // GET Home/Index
    public IActionResult Index() => View();
}