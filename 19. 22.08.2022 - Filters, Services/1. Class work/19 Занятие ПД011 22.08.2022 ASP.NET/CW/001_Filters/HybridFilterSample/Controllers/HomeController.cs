using HybridFilterSample.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HybridFilterSample.Controllers;

public class HomeController : Controller
{
    // кастомный фильтр метода действия (включает в себя фильтр результата)
    [Profile]
    public IActionResult Index() => View();

    // кастомный фильтр метода действия (включает в себя фильтр результата)
    [Profile]
    public IActionResult Action1() {
        // имитация продолжитеьной обработки
        // в процессе выполнения метода действия 
        Thread.Sleep(2_000);
        return View();
    } // Action1

    // кастомный фильтр метода действия (включает в себя фильтр результата)
    [Profile]
    public IActionResult Action2() {
        return View();
    } // Action2
}
