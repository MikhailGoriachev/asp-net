using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models;
using Home_work.ViewModels;

namespace Home_work.Components;

public class TodosComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public TodosComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Список пользователей
        List<Todo> todos = await _generalModelService.GetTodoAsync();

        //Формирум view model
        TodoViewModel viewModel = new TodoViewModel(todos);

        return View(viewModel);
    }

}
