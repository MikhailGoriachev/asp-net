using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

public class PersonsController : Controller
{
    public IActionResult Index() => View();
}