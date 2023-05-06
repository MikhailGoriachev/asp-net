using ActionFilterSample.Infrastructure; // подключение пространства имен атрибутов-фильтров
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ActionFilterSample.Controllers;

public class HomeController : Controller
{
    // кастомный фильтр метода действия
    [Profile]
    public IActionResult Index() => View();

    // кастомный фильтр метода действия
    [Profile]
    public IActionResult Action1() {
        // имитация продолжитеьной обработки
        // в процессе выполнения метода действия 
        Thread.Sleep(2_000);
        return View();
    } // Action1

    // кастомный фильтр метода действия
    [Profile]
    public IActionResult Action2() {
        return View();
    } // Action2
} // class HomeController
