using Microsoft.AspNetCore.Mvc;

namespace UsingTagHelpers.Controllers;

// Для домашней страницы особой раюоты нет - просто вывод задания
public class HomeController : Controller
{
    public IActionResult Index() => View();

} // class HomeController
