using System.Text.Encodings.Web;
using System.Web;
using HomeWork.Models;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Components;

// Компонент для вывода подробной информации о персоне
public class PersonInfoComponent : ViewComponent
{
    // сервис для доступа к данным
    public StarWarsService DataService { get; set; }


    // конструктор инициализирующий
    public PersonInfoComponent(StarWarsService dataService)
    {
        DataService = dataService;
    }


    // работа компонента
    public async Task<IViewComponentResult> InvokeAsync(string url) =>
        View(await DataService.GetPersonUrlAsync(url));
}
