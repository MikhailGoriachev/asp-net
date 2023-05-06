using Microsoft.AspNetCore.Mvc;

namespace AreaPractice.Controllers;

// Для домашней страницы особой работы нет - просто вывод задания
public class HomeController : Controller
{
    public IActionResult Index() => View();

} // class HomeController
