using Microsoft.AspNetCore.Mvc;
using ViewComponentsFirst.Model;

namespace ViewComponentsFirst.Components;

// Асинхронный компонент для работы с коллекцией пользователей
public class RegistredUsers : ViewComponent
{
    private readonly UserService _service;

    // получить связь с данными через внедрение зависимостей 
    public RegistredUsers(UserService service) {
        _service = service;
    } // UserService

    // Реализация логики компонента
    public async Task<IViewComponentResult> InvokeAsync() {
        // получение данных должно быть асинхронным
        List<User> users = await _service.GetUsersAsync();
        
        // имитация обработки в компоненте - вернем количество пользователей
        ViewBag.Count = users.Count;

        return View(users);
    } // InvokeAsync
} // class RegistredUsers

