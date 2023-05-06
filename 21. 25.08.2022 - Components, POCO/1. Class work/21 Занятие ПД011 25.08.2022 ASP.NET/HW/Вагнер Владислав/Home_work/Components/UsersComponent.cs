using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models;
using Home_work.ViewModels;

namespace Home_work.Components;

public class UsersComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public UsersComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Список пользователей
        List<User> users = await _generalModelService.GetUsersAsync();

        //Формирум view model
        UsersViewModel viewModel = new UsersViewModel(users);

        return View(viewModel);
    }

}
