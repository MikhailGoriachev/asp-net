using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class PlanetsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}