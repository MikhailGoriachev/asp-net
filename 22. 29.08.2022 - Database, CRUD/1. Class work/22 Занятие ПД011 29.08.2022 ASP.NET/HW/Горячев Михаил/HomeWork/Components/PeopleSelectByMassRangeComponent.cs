using HomeWork.Models.BindingModels;
using HomeWork.Models.ViewModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода всех персон выбранных по диапазону массы
public class PeopleSelectByMassRangeComponent : ViewComponent
{
    // сервис с данными
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PeopleSelectByMassRangeComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync(RangeBindingModel model)
    {
        var data = await DataService.GetPersonsAsync();

        if (model.Min >= 0 && model.Max > 0 && model.Min < model.Max)
            data.Persons = data.Persons.Where(p => p.Mass >= model.Min && p.Mass <= model.Max).ToList();

        return View(new PeopleListViewModel(data?.Persons?.OrderBy(p => p.Mass).ToList(),
            (data?.Persons?.Min(p => p.Mass),
                data?.Persons?.Average(p => p.Mass),
                data?.Persons?.Max(p => p.Mass))));
    }
}
