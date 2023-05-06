using Microsoft.AspNetCore.Mvc;
using ViewComponentsFirst.Model;

namespace ViewComponentsFirst.Components;

// Асинхронный компонент для работы с коллекцией запланированных дел пользователя 
public class UserToDos : ViewComponent
{
    private readonly UserService _service;

    // получить связь с данными через внедрение зависимостей 
    public UserToDos(UserService service) {
        _service = service;
    } // UserToDos

    // Реализация логики компонента
    public async Task<IViewComponentResult> InvokeAsync(int id) {
        // получение данных должно быть асинхронным
        List<Todo> todos = await _service.GetUserTodosAsync(id);

        // количество дел, количество завершенных и не завершенных дел
        int completed = todos.Count(t => t.Completed);
        ViewBag.Statistics = (
            todos.Count,
            completed,
            todos.Count - completed
         );

        return View(todos);
    } // InvokeAsync
} // class UserToDos

