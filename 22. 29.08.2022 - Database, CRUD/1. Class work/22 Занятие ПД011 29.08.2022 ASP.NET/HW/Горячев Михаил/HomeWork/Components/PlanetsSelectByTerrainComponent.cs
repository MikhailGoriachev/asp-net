using HomeWork.Models.BindingModels;
using HomeWork.Models.ViewModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода всех планет выбранных по типу поверхности
public class PlanetsSelectByTerrainComponent : ViewComponent
{
    // сервис с данными
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PlanetsSelectByTerrainComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync(string terrain)
    {
        var data = await DataService.GetPlanetsAsync();

        if (terrain != "")
            data!.Planets = data.Planets?.Where(p => p.Terrain!.Contains(terrain)).ToList();


        return View(new PlanetsViewModel(data?.Planets,
            (data?.Planets?.Min(p => p.Diameter),
                data?.Planets?.Average(p => p.Diameter),
                data?.Planets?.Max(p => p.Diameter)),
            (data?.Planets?.Min(p => p.OrbitalPeriod),
                data?.Planets?.Average(p => p.OrbitalPeriod),
                data?.Planets?.Max(p => p.OrbitalPeriod))));
    }
}
