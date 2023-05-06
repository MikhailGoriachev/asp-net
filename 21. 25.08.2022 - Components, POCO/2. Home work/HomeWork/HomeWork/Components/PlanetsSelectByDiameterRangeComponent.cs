using HomeWork.Models.BindingModels;
using HomeWork.Models.ViewModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода всех планет выбранных по диапазону диаметра
public class PlanetsSelectByDiameterRangeComponent : ViewComponent
{
    // сервис с данными
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PlanetsSelectByDiameterRangeComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync(RangeBindingModel range)
    {
        var data = await DataService.GetPlanetsAsync();

        if (range.Min >= 0 && range.Max > 0  && range.Min < range.Max)
            data!.Planets = data.Planets?.Where(p => p.Diameter >= range.Min && p.Diameter <= range.Max).ToList();

        return View(new PlanetsViewModel(data?.Planets,
            (data?.Planets?.Min(p => p.Diameter),
                data?.Planets?.Average(p => p.Diameter),
                data?.Planets?.Max(p => p.Diameter)),
            (data?.Planets?.Min(p => p.OrbitalPeriod),
                data?.Planets?.Average(p => p.OrbitalPeriod),
                data?.Planets?.Max(p => p.OrbitalPeriod))));
    }
}
