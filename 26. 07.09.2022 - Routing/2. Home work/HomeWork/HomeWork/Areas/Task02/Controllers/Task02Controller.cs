using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Areas.Task02.Controllers;

[Area("Task02")]
[Route("[area]/[controller]")]
public class Task02Controller : Controller
{
    // выражение 3
    [Route("[action]")]
    public IActionResult Expression03() => View(new Expression03(null, null));

    [HttpPost]
    [Route("[action]")]
    public IActionResult Expression03(Expression03 expr) => View(expr);

}
