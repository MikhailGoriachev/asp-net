using Microsoft.AspNetCore.Mvc;

namespace H_WASP_NET.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
