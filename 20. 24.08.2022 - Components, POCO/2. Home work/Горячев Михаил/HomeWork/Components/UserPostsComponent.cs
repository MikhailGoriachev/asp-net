using HomeWork.Models;
using HomeWork.Models.ViewModels;
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

        return View(new UserPostsViewModel(posts, posts.Count(),
            posts.Max(p => p.Body.Length),
            (int)posts.Average(p => p.Body.Length),
            posts.Min(p => p.Body.Length)));
    }
}
