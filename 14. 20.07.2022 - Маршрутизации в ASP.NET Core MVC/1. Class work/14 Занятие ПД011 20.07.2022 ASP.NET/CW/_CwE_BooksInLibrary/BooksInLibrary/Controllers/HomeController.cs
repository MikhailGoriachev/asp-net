using Microsoft.AspNetCore.Mvc;

namespace BooksInLibrary.Controllers;

public class HomeController : Controller 
{
    // Вывод главной страницы
    // GET /Home/Index
    public IActionResult Index() => View();

}

