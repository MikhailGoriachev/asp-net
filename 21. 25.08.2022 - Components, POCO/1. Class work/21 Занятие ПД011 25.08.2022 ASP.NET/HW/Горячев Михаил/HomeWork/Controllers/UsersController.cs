using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class UsersController : Controller
{
    // все пользователи
    public IActionResult Index()
    {
        return View();
    }


    // посты пользователя
    public IActionResult UserPosts(int id) => View(id);

    // фото пользователя
    public IActionResult UserPhotos(int id) => View("UserPhotos", id);

    // дела пользователя
    public IActionResult UserTodos(int id) => View("UserTodos", id);
}
