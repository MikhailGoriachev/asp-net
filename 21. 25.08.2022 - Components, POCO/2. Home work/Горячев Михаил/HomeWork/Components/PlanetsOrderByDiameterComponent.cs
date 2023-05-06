using HomeWork.Models.ViewModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для сортировки и вывода всех планет по убыванию диаметра
public class PlanetsOrderByDiameterComponent : ViewComponent
{
    // сервис с данными
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PlanetsOrderByDiameterComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await DataService.GetPlanetsAsync();

        return View(new PlanetsViewModel(data?.Planets?.OrderByDescending(p => p.Diameter).ToList(),
            (data?.Planets?.Min(p => p.Diameter),
                data?.Planets?.Average(p => p.Diameter),
                data?.Planets?.Max(p => p.Diameter)),
            (data?.Planets?.Min(p => p.OrbitalPeriod),
                data?.Planets?.Average(p => p.OrbitalPeriod),
                data?.Planets?.Max(p => p.OrbitalPeriod))));
    }
}
