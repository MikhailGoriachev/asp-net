using Master.Common;
using Microsoft.AspNetCore.Mvc;


namespace Master.Controllers;


public sealed class HomeController : Controller
{
    [HttpGet(Routes.Home.Index)]
    public IActionResult Index() => View();
}