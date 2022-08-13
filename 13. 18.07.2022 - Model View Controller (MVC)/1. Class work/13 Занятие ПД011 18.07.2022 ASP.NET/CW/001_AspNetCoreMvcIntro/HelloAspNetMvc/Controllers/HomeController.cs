using Microsoft.AspNetCore.Mvc;

namespace HelloAspNetMvc.Controllers
{
    // простейший контроллер на одно действие :)
    public class HomeController : Controller
    {
        // GET  /Home/Index
        public IActionResult Index() {
            return View();
        } // Index
    } //  class HomeController
}
