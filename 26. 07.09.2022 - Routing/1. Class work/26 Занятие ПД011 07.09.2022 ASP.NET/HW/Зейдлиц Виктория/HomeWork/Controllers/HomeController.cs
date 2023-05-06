using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    
}
