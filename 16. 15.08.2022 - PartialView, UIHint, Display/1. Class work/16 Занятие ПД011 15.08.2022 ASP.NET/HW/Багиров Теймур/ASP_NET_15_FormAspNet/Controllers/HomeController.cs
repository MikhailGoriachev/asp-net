using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_15_FormAspNet.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()  {
        return View();
    }
    
    
    
}