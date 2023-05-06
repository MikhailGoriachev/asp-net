using Microsoft.AspNetCore.Mvc;

namespace GlobalFilters.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        

        public IActionResult Action1()
        {
            throw new System.Exception("My Test Exception");
            // return View();
        }

        public IActionResult Action2()
        {
            throw null;
            // return View();
        }
    }
}