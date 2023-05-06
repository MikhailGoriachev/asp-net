using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace HomeWork.Components;

// Компонент для вывода дел пользователя
public class UserTodosComponent : ViewComponent
{
    // сервис для получения данных о пользователях
    public UsersService UsersService { get; set; }


    // конструктор инициализирующий
    public UserTodosComponent(UsersService usersService)
    {
        UsersService = usersService;
    }


    // метод действия компонента
    public async Task<ViewViewComponentResult> InvokeAsync(int id)
    {
        var todos = await UsersService.GetUserTodosAsync(id);

        ViewBag.CompletedCount = todos.Count(t => t.Completed);
        ViewBag.Count = todos.Count();

        return View(todos);
    }
}
