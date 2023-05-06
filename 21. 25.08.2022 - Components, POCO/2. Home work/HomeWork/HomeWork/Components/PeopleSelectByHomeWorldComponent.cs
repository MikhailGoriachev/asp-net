using HomeWork.Models.BindingModels;
using HomeWork.Models.ViewModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода всех персон выбранных по родному миру
public class PeopleSelectByHomeWorldComponent : ViewComponent
{
    // сервис с данными
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PeopleSelectByHomeWorldComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync(string homeWorld)
    {
        var data = await DataService.GetPersonsAsync();

        if (homeWorld != "")
            data.Persons = data.Persons.Where(p => p.HomeWorld == homeWorld).ToList();

        return View(new PeopleListViewModel(data?.Persons?.OrderBy(p => p.Mass).ToList(),
            (data?.Persons?.Min(p => p.Mass),
            data?.Persons?.Average(p => p.Mass),
            data?.Persons?.Max(p => p.Mass))));
    }
}
