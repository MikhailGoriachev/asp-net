using HomeWork.Models.ViewModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace HomeWork.Components;

// Компонент для вывода всех планет
public class PlanetListComponent : ViewComponent
{
    // сервис с данными
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PlanetListComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await DataService.GetPlanetsAsync();

        return View(new PlanetsViewModel(data?.Planets,
            (data?.Planets?.Min(p => p.Diameter),
                data?.Planets?.Average(p => p.Diameter),
                data?.Planets?.Max(p => p.Diameter)),
            (data?.Planets?.Min(p => p.OrbitalPeriod),
                data?.Planets?.Average(p => p.OrbitalPeriod),
                data?.Planets?.Max(p => p.OrbitalPeriod))));
    }
}
