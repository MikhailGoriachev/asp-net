using Microsoft.AspNetCore.Mvc;

namespace ActionResultsSamples.Controllers;

public class RedirectsController : Controller
{
    public IActionResult Index() {
        return View();
    }

    public IActionResult Sample1() {
        // перенаправление с помощью кода 302. Временное перенаправление.
        return Redirect("https://donstep.com");
    }

    public IActionResult Sample2() {
        // перенаправление с помощью кода 302. Временное перенаправление.
        return Redirect("/home/index");
    }

    public IActionResult Sample3() {
        // перенаправление с помощью кода 301. Постоянное перенаправление. 
        // Обычно применяют при SEO-оптимизации сайта (search engine optimization)
        return RedirectPermanent("/home/index");
    }

    // перенаправление на метод Index текущего контроллера
    public IActionResult Sample4() {
        return RedirectToAction("Index");
    }

    // перенаправление на метод Index контроллера Home
    public IActionResult Sample5() {
        return RedirectToAction("Index", "Home");
    }

    // Перенаправление с использованием значений для переменных сегментов
    public IActionResult Sample6() {
        return RedirectToRoute(new { controller = "home", action = "index" });
    }
}