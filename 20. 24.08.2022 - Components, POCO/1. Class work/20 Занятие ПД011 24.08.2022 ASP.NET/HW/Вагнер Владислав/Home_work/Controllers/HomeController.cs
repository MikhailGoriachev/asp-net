using Microsoft.AspNetCore.Mvc;

namespace Home_work.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
