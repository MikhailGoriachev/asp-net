using HomeWork.Models;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода подробной информации о планете
public class PlanetInfoComponent : ViewComponent
{
    // сервис для доступа к данным
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PlanetInfoComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонентаs
    public async Task<IViewComponentResult> InvokeAsync(string url) =>
        View(await DataService.GetDataAsync<Planet>(url));
}
