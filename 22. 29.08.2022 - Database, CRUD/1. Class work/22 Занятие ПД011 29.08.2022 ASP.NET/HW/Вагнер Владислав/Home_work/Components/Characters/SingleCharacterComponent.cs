using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models.Characters;
using Home_work.Models;
using Home_work.ViewModels;
using System.Text;
using System.Web;


namespace Home_work.Components.Characters;

public class SingleCharacterComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public SingleCharacterComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync(string url)
    {
        url = HttpUtility.UrlDecode(url);

        Character character = await _generalModelService.GetCharacterAsync(url);

        //Получение названия планеты
        character.Homeworld = _generalModelService.GetDataAsync<PlanetName>(character.Homeworld).Result.Name;

        return View(character);
    }

}
