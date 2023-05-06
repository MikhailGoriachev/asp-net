using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers
{
    public class HomeController : Controller
    {
        // страница "Главная"
        public IActionResult Index()
        {
            ViewBag.Title = "Главная";

            return View();
        }
    }
}
