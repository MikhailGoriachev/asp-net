using DIServiceFilterSample.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DIServiceFilterSample.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();


    // ServiceFilter - фильтр, который находит другой фильтр используя
    // IServiceProvider (внедрение зависимостей)
    [ServiceFilter(typeof(ExceptionLogingAttribute))]
    public IActionResult Action1() {
        throw new System.Exception("My Test Exception"); 
        // return View();
    }

    [ServiceFilter(typeof(ExceptionLogingAttribute))]
    public IActionResult Action2() {
        throw null;
        // return View();
    }
}
