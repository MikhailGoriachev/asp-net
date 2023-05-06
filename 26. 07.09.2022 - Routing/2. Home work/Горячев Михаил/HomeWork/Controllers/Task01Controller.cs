using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

[Route("[controller]")]
public class Task01Controller : Controller
{
    // выражение 1
    [Route("[action]/{x:double=1}/{y:double=1}/{z:double=3}")]
    public IActionResult Expression01(double x, double y, double z) =>
        View(new Expression01(x, y, z));

    // выражение 2
    [Route("[action]/{x:double=3}/{y:double=4}/{z:double=5}")]
    public IActionResult Expression02(double x, double y, double z)=>
        View(new Expression02(x, y, z));
}
