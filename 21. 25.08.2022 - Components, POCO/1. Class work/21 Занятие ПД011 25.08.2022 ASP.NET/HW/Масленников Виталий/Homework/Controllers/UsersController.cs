using Microsoft.AspNetCore.Mvc;

namespace Homework.Controllers;

public class UsersController : Controller
{
    // GET Index
    public IActionResult Index() => View();
    
    // GET Посты пользователя
    public IActionResult Posts(int id) => View(id);

    // GET Фото пользователя
    public IActionResult Photos(int id) => View("Photos", id);
    
    // GET Задачи пользователя
    public IActionResult Tasks(int id) => View("Tasks", id);
}