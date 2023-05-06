using HomeWork.Models.ViewModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода всех персон
public class PeopleListComponent : ViewComponent
{
    // сервис с данными
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PeopleListComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await DataService.GetPersonsAsync();

        return View(new PeopleListViewModel(data?.Persons, (data.Persons.Min(p => p.Mass),
                data.Persons.Average(p => p.Mass),
            data.Persons.Max(p => p.Mass))));
    }
}
