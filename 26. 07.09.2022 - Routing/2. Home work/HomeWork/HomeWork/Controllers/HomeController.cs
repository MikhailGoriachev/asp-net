using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

[Route("{controller=Home}")]
public class HomeController : Controller
{
    // страница "Главная"
    [Route("{action=Index}")]
    public IActionResult Index() => View();
}
