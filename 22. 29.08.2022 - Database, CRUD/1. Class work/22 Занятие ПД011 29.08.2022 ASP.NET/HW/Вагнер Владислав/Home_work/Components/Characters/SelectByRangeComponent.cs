using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models.Characters;
using Home_work.Models;
using Home_work.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_work.Components.Characters;

public class SelectByRangeComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public SelectByRangeComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync(RangeModel rangeModel)
    {
        //Список пользователей
        CharactersDeserializationModel characters = await _generalModelService.GetCharactersAsync();

        characters.Characters = characters.Characters.Where(c => c.WeightDouble >= rangeModel.Min && c.WeightDouble <= rangeModel.Max).ToList();

        //Вычисления
        double? avgWeight = characters.Characters.Average(c => c.WeightDouble);
        double? delta = characters.Characters.Max(c => c.WeightDouble) - characters.Characters.Min(c => c.WeightDouble);

        //Для выборки по планетам
        ViewBag.SelectPlanets = new SelectList(characters.Characters.Select(c => c.Homeworld).Distinct());

        //Формируем view model
        CharactersViewModel viewModel = new CharactersViewModel(characters.Characters, characters.Characters.Count, (avgWeight, delta));

        return View(viewModel);
    }

}
