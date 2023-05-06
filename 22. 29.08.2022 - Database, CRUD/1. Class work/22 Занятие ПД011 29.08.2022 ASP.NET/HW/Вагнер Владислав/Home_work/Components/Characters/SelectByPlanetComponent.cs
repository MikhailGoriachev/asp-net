using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models.Characters;
using Home_work.Models;
using Home_work.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_work.Components.Characters;

public class SelectByPlanetComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public SelectByPlanetComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync(string planet)
    {
        //Список пользователей
        CharactersDeserializationModel characters = await _generalModelService.GetCharactersAsync();
        //Для выборки по планетам
        ViewBag.SelectPlanets = new SelectList(characters.Characters.Select(c => c.Homeworld).Distinct());

        characters.Characters = characters.Characters.Where(c => c.Homeworld == planet).ToList();

        //Вычисления
        double? avgWeight = characters.Characters.Average(c => c.WeightDouble);
        double? delta = characters.Characters.Max(c => c.WeightDouble) - characters.Characters.Min(c => c.WeightDouble);


        //Формируем view model
        CharactersViewModel viewModel = new CharactersViewModel(characters.Characters, characters.Characters.Count, (avgWeight, delta));

        return View(viewModel);
    }

}
