using Microsoft.AspNetCore.Mvc;

namespace ViewComponentsFirst.Controllers;

public class UsersController : Controller
{
    // вывод списка пользователей
    public IActionResult Index() {
        return View();
    }

    // Вывод страницы с постами пользователя
    public IActionResult UserPosts(int id) {
        return View(id);
    } // UserPosts


    // Вывод страницы с фотографиями пользователя с идентификатором id
    public IActionResult UserPhotos(int id) {
        return View(id);
    } // UserPhotos


    // Вывод страницы с запланированными делами пользователя
    public IActionResult UserToDos(int id) {
        return View(id);
    } // UserToDos
}

