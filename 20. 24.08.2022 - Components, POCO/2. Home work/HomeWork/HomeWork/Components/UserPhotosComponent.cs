using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace HomeWork.Components;

// Компонент для вывода фотографий пользователя
public class UserPhotosComponent : ViewComponent
{
    // сервис для получения данных о пользователях
    public UsersService UsersService { get; set; }


    // конструктор инициализирующий
    public UserPhotosComponent(UsersService usersService)
    {
        UsersService = usersService;
    }


    // метод действия компонента
    public async Task<ViewViewComponentResult> InvokeAsync(int id)
    {
        var photos = await UsersService.GetUserPhotosAsync(id);

        // количество фото
        return View(new UserPhotosViewModel(photos, photos.Count()));
    }
}
