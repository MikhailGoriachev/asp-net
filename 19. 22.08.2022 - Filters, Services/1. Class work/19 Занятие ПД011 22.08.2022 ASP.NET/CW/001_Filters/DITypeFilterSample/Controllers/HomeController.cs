using DITypeFilterSample.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DITypeFilterSample.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();


    // TypeFilter - фильтр, который позволяет создать другой фильтр,
    // используя для его создания пользовательский конструктор.
    // При этом, значения, для конструктора, будут предоставляться
    // через внедрение зависимостей
    [TypeFilter(typeof(ExceptionLogingAttribute))]
    public IActionResult Action1() {
        throw new System.Exception("My Test Exception");
        // return View();
    }

    [TypeFilter(typeof(ExceptionLogingAttribute))]
    public IActionResult Action2() {
        throw null;
        // return View();
    }
}
