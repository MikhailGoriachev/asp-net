using Microsoft.AspNetCore.Mvc;

namespace UiHintDisplay.Controllers;

// вывод сведений о приложении и разработчике
public class HomeController : Controller
{
    public IActionResult Index() => View();
} // class HomeController

