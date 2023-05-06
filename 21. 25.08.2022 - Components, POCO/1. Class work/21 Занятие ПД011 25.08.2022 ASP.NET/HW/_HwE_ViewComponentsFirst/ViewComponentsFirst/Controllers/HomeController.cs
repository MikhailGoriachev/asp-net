using Microsoft.AspNetCore.Mvc;

namespace ViewComponentsFirst.Controllers;

public class HomeController : Controller
{
    // GET /Home/Index
    public IActionResult Index() {
        return View();
    }


}
