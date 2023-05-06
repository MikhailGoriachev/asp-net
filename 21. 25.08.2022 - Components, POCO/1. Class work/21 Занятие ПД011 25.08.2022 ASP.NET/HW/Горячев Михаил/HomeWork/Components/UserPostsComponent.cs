using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace HomeWork.Components;

// Компонент для вывода постов пользователя
public class UserPostsComponent : ViewComponent
{
    // сервис для получения данных о пользователях
    public UsersService UsersService { get; set; }


    // конструктор инициализирующий
    public UserPostsComponent(UsersService usersService)
    {
        UsersService = usersService;
    }


    // метод действия компонента
    public async Task<ViewViewComponentResult> InvokeAsync(int id)
    {
        var posts = await UsersService.GetUserPostsAsync(id);

        ViewBag.Count = posts.Count();
        ViewBag.MaxSize = posts.Max(p => p.Body.Length);
        ViewBag.AvgSize = posts.Average(p => p.Body.Length);
        ViewBag.MinSize = posts.Min(p => p.Body.Length);

        return View(posts);
    }
}
