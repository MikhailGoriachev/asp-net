using Microsoft.AspNetCore.Mvc;

namespace EfAspCoreMvciltersPaginations.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
