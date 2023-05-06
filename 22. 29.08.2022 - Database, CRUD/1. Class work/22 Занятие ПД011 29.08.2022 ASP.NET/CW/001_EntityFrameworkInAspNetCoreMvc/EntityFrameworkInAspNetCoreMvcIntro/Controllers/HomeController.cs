using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkInAspNetCoreMvcIntro.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
