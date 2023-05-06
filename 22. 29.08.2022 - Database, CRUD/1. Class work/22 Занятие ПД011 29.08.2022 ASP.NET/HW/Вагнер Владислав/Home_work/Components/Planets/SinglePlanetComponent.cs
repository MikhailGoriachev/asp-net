using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models.Planets;
using Home_work.Models;
using Home_work.ViewModels;
using System.Text;
using System.Web;


namespace Home_work.Components.Planets;

public class SinglePlanetComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public SinglePlanetComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync(string url)
    {
        url = HttpUtility.UrlDecode(url);

        Planet planet = await _generalModelService.GetPlanetAsync(url);

        return View(planet);
    }

}
