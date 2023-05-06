using AspNetCoreMvcFirst.Models.Task1;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcFirst.Controllers;
public class Task1Controller : Controller
{
    // Объект для организации вычислений
    public Calculate Calculate { get; set; } = new();

    // GET Task1/Index - вывод формулировки задачи
    public IActionResult Index() => View();


    // вычисления по рисунку 1
    // GET Task1/Expression1
    public IActionResult Expression1(){

        // исходные данные и вычисление
        Calculate.X = 1;
        Calculate.Y = 1;
        Calculate.Z = 3;

        (double a, double b) = Calculate.Calc1();

        // передача части данных в представление через ViewBag
        ViewBag.Header = "Выражение 1";
        
        // контрольные значения результов вычислений
        ViewBag.Data = (Calculate.X, Calculate.Y, Calculate.Z);
        ViewBag.A = 9.608184;
        ViewBag.B = 2.962605;
        return View("Expression", new Result(a, b));
    } // Expression1


    // вычисления по рисунку 2
    public IActionResult Expression2() {

        // исходные данные и вычисление
        Calculate.X = 3;
        Calculate.Y = 4;
        Calculate.Z = 5;
        (double a, double b) = Calculate.Calc2();

        // передача части данных в представление через ViewBag
        ViewBag.Header = "Выражение 2";

        // контрольные значения результатов вычислений
        ViewBag.Data = (Calculate.X, Calculate.Y, Calculate.Z);
        ViewBag.A = 3.288716;
        ViewBag.B = 0.9615385;
        return View("Expression", new Result(a, b));
    } // Expression2
} // class Task1Controller
