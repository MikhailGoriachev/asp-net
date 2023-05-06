using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;


public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index() => View();
}