using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class Task1Controller : Controller
{
    // выражение 1
    public IActionResult Expression1()
    {
        ViewBag.Title = "Первое выражение";

        // исходные данные
        const double x = 1, y = 1, z = 3;

        return View(new Expression1(x, y, z));
    }

    // выражение 2
    public IActionResult Expression2()
    {
        ViewBag.Title = "Второе выражение";

        // исходные данные
        const double x = 3, y = 4, z = 5;

        return View(new Expression2(x, y, z));
    }
}
