using ExceptionFilterSample.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionFilterSample.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();


    [ExceptionLoging] // перехват исключения фильтром 
    public IActionResult Action1() {
        throw new System.Exception($"Выброс исключения из метода Action1 {DateTime.Now:G}");

        // да, этот оператор никогда не выполнится
        return View();
    } // Action2


    // нет атрибута - нет перехвата исключения
    // в этом действии выбрасывается исключение, которое не перехватывается фильтром
    public IActionResult Action2() {
        throw new System.NullReferenceException("Выброс исключения из метода Action2");

        // да, этот оператор никогда не выполнится
        return View();
    } // Action2
} // class HomeController
