using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class PeopleController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}