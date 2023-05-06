using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models.Characters;
using Home_work.Models;
using Home_work.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_work.Components.Characters;

public class OrderByWeightComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public OrderByWeightComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Список пользователей
        CharactersDeserializationModel characters = await _generalModelService.GetCharactersAsync();

        //Сортировка по весу
        characters.Characters = characters.Characters.OrderBy(c => c.WeightDouble).ToList();

        //Вычисления
        double? avgWeight = characters.Characters.Average(c => c.WeightDouble);
        double? delta = characters.Characters.Max(c => c.WeightDouble) - characters.Characters.Min(c => c.WeightDouble);

        //Для выборки по планетам
        ViewBag.SelectPlanets = new SelectList(characters.Characters.Select(c => c.Homeworld).Distinct());

        //Формируем view model
        CharactersViewModel viewModel = new CharactersViewModel(characters.Characters, characters.Characters.Count, (avgWeight, delta));

        //Вызов default представления
        return View(viewModel);
    }

}
