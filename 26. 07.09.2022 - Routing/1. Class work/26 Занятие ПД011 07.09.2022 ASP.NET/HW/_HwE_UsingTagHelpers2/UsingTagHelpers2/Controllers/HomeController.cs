using Microsoft.AspNetCore.Mvc;

namespace UsingTagHelpers2.Controllers;

// Для домашней страницы особой работы нет - просто вывод задания
public class HomeController : Controller
{
    public IActionResult Index() => View();

} // class HomeController
