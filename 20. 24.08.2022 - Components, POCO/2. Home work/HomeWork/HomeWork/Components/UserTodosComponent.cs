using HomeWork.Models;
using HomeWork.Models.ViewModels;
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

        int completedCount = todos.Count(t => t.Completed);
        int count = todos.Count();

        return View(new UserTodosViewModel(todos, count, completedCount, count - completedCount));
    }
}
