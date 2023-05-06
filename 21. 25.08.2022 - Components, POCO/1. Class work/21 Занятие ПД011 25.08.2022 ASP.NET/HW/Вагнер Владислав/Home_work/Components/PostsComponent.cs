using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models;
using Home_work.ViewModels;

namespace Home_work.Components;

public class PostsComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public PostsComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Список пользователей
        List<Post> posts = await _generalModelService.GetPostsAsync();

        //Формирум view model
        PostViewModel viewModel = new PostViewModel(posts);

        return View(viewModel);
    }

}
