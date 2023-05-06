using Microsoft.AspNetCore.Mvc;

namespace Homework.Areas.Task2.Controllers;

[Area("Task2")]
[Route("Task2")]
public class HomeController : Controller
{
    public IActionResult Index() => View();
    
    [HttpPost("Calc")]
    public IActionResult Calc([FromForm]double z1, [FromForm]double z2)
    {
        ViewBag.z1 = z1;
        ViewBag.z2 = z2;

        return View("Index");
    }
}