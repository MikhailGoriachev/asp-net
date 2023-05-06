using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

public class PlanetsController : Controller
{
    public IActionResult Index() => View();
}