using AreaPractice.Areas.Task2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AreaPractice.Areas.Task2.Controllers;

// Решение задачи 2

// атрибут для связи контроллера с областью
[Area("Task2")]
public class TaskController : Controller
{
    public Variant13 Variant13 { get; set; } = new();

    // GET-запрос, вывод разметки для ввода
    public IActionResult CalcVariant13() {
        return View((double.NaN, double.NaN));
    } // CalcVariant13

    // обработка ввода мз формы, вывод результатов 
    [HttpPost]
    public IActionResult CalcVariant13(double a, double b) {

        // получить данные из формы
        Variant13.A = a;
        Variant13.B = b;
        ViewBag.Data = (a, b);

        // вычислить и вернуть результат
        return View(Variant13.Calc());
    } // Variant13
} //  class Task2Controller
