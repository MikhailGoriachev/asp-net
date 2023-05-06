using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models;
using Home_work.ViewModels;

namespace Home_work.Components;

public class AlbumComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public AlbumComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Список пользователей
        List<Photo> photos = await _generalModelService.GetAlbumAsync();

        //Формирум view model
        AlbumViewModel viewModel = new AlbumViewModel(photos);

        return View(viewModel);
    }

}
