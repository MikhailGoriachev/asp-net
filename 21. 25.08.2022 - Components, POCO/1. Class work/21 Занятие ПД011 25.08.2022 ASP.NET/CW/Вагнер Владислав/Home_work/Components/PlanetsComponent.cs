using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models;
using Home_work.ViewModels;

namespace Home_work.Components;

public class PlanetsComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public PlanetsComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Список пользователей
        List<Planet> posts = await _generalModelService.GetPostsAsync();

        //Формирум view model
        PlanetsViewModel viewModel = new PlanetsViewModel(posts);

        return View(viewModel);
    }

}
