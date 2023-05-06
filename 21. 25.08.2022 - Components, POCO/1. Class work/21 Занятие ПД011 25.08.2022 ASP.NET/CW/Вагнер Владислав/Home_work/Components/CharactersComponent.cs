using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Home_work.Models.Characters;
using Home_work.Models;
using Home_work.ViewModels;

namespace Home_work.Components;

public class CharactersComponent : ViewComponent
{
    private GeneralModelService _generalModelService;

    //ctor с внедрением зависимостей
    public CharactersComponent(GeneralModelService modelService)
    {
        _generalModelService = modelService;
    }

    //Асинхронный запуск компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //Список пользователей
        CharactersDeserializationModel characters = await _generalModelService.GetCharactersAsync();
        var tempList = new List<Character>();

        while (characters.Results.Count < 41)
        {
            tempList = _generalModelService.GetCharactersAsync(characters.Next).Result.Results;
            characters.Results.AddRange(tempList);
        }

        int id = 0;
        characters.Results.ForEach(c => c.Id = ++id);

        //Формируем view model
        CharactersViewModel viewModel = new CharactersViewModel(characters.Results, characters.Results.Count);

        return View(viewModel);
    }

}
