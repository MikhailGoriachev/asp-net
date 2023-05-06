using Microsoft.AspNetCore.Mvc;

namespace PartialViews01.Controllers;


// контроллер сюжетно не важен
public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Catalog() => View();

    public IActionResult Contacts() => View();

} // class HomeController
