using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода всех персон
public class People : ViewComponent
{
    // сервис с данными
    public StartWarsService DataService { get; set; }


    // конструктор инициализирующий
    public People(StartWarsService dataService)
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
