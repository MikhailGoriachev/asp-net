using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class UsersController : Controller
{
    // все пользователи
    public IActionResult Index() => View();

    // посты пользователя
    public IActionResult UserPosts(int id) => View(id);

    // фото пользователя
    public IActionResult UserPhotos(int id) => View(id);

    // дела пользователя
    public IActionResult UserTodos(int id) => View(id);
}
