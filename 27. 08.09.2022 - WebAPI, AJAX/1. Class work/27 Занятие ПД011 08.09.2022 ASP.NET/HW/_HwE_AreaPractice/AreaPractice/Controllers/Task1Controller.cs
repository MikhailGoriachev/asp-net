using AreaPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace AreaPractice.Controllers;

// Контроллер для решения задачи 1
public class Task1Controller : Controller
{
    // Объект для организации вычислений
    public Calculate Calculate { get; set; } = new();


    // вычисления по рисунку 1
    // GET /Expression1
    [Route("Expression1")]
    public IActionResult Expression1() {

        // исходные данные и вычисление
        Calculate.X = 1;
        Calculate.Y = 1;
        Calculate.Z = 3;

        Result result = Calculate.Calc1();

        // передача части данных в представление через ViewBag
        ViewBag.Header = "Выражение 1";
        
        // контрольные значения результов вычислений
        ViewBag.Data = (Calculate.X, Calculate.Y, Calculate.Z);
        ViewBag.Check = new Result(9.608184, 2.962605);
        return View("Expression", result);
    } // Expression1


    // вычисления по рисунку 2
    // GET /Expression2
    [Route("Expression2")]
    public IActionResult Expression2() {

        // исходные данные и вычисление
        Calculate.X = 3;
        Calculate.Y = 4;
        Calculate.Z = 5;
        Result result = Calculate.Calc2();

        // передача части данных в представление через ViewBag
        ViewBag.Header = "Выражение 2";

        // контрольные значения результатов вычислений
        ViewBag.Data = (Calculate.X, Calculate.Y, Calculate.Z);
        ViewBag.Check =new Result(3.288716, 0.9615385);
        return View("Expression", result);
    } // Expression2
} // class Task1Controller
