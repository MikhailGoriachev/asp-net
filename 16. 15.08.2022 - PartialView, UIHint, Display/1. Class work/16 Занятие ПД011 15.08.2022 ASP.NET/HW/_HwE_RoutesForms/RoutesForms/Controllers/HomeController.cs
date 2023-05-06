using Microsoft.AspNetCore.Mvc;

namespace RoutesForms.Controllers;

public class HomeController : Controller
{
    // Вывод главной страницы
    // GET /Home/Index
    public IActionResult Index() => View();

}

