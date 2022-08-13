using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

public class Home : Controller
{
    // GET Home/Index
    public IActionResult Index() => View();
}