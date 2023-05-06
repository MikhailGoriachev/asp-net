using System.Web;
using HomeWork.Models.BindingModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Controllers;

public class PlanetsController : Controller
{
    // сервис для доступа к данным
    public StarWarsService DataService { get; set; }


    #region Конструкторы

    // конструктор инициализирующий
    public PlanetsController(StarWarsService dataService)
    {
        DataService = dataService;
    }

    #endregion

    #region Методы действия

    // все записи
    public IActionResult Index() => View();


    // подробная информация о планете
    public IActionResult PlanetInfo(string url) => View((object)HttpUtility.UrlDecode(url));


    // упорядочивание по убыванию диаметра
    public IActionResult OrderByDiameter() => View();


    // выборка по диаметру в выбранном диапазоне
    public IActionResult SelectByDiameterRange() => View(new RangeBindingModel { Min = 0, Max = 0 });

    [HttpPost]
    public IActionResult SelectByDiameterRange(RangeBindingModel model)
    {
        if (model.Min > model.Max)
            ModelState.AddModelError("Min", "Минимальное значение не может быть больше максимального!");

        return View(model);
    }


    // выборка по типу поверхности
    public async Task<ViewResult> SelectByTerrainAsync()
    {
        ViewBag.TerrainList = new SelectList((await DataService.GetPlanetTerrainListAsync())!);

        return View(new StringBindingModel(""));
    }

    [HttpPost]
    public async Task<ViewResult> SelectByTerrainAsync(StringBindingModel model)
    {
        ViewBag.TerrainList = new SelectList(await DataService.GetPlanetTerrainListAsync());

        return View(new StringBindingModel(model.Value));
    }

    #endregion
}
