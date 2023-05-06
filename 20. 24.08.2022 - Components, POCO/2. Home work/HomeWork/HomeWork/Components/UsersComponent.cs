using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace HomeWork.Components;

// Компонент для вывода всех пользователей
public class UsersComponent : ViewComponent
{
    // сервис для получения данных о пользователях
    public UsersService UsersService { get; set; }


    // конструктор с внедрением зависимостей
    public UsersComponent(UsersService usersService) =>
        UsersService = usersService;


    // метод действия компонента
    public async Task<ViewViewComponentResult> InvokeAsync()
    {
        var data = await UsersService.GetUsersAsync();
        return View(new UsersViewModel(data, data.Count()));
    }
}
