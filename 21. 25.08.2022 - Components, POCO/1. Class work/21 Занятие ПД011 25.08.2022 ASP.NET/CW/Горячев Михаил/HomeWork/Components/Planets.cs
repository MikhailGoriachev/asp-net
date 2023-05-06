using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace HomeWork.Components;

// Компонент для вывода всех планет
public class Planets : ViewComponent
{
    // сервис с данными
    public StartWarsService DataService { get; set; }


    // конструктор инициализирующий
    public Planets(StartWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var data = await DataService.GetPlanetsAsync();

        ViewBag.Count = data.Count;

        return View(data);
    }
}
