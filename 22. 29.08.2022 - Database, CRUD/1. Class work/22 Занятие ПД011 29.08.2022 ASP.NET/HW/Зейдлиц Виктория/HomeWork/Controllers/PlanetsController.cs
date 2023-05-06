using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class PlanetsController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Header = "Коллекция планет.";

        return View();
    }


} // class PlanetsController
