using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcFirst.Controllers;

public class HomeController : Controller 
{
    public IActionResult Index() => View();
}

