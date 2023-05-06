using System.Web;
using HomeWork.Models.BindingModels;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Controllers;

public class PeopleController : Controller
{
    // сервис для доступа к данным
    public StarWarsService DataService { get; set; }


    #region Конструкторы

    // конструктор инициализирующий
    public PeopleController(StarWarsService dataService)
    {
        DataService = dataService;
    }

    #endregion


    #region Методы действия

    // все записи
    public IActionResult Index() => View();


    // получить подробную информацию
    public IActionResult PersonInfo(string url) => View((object)HttpUtility.UrlDecode(url));


    // упорядочивание позрастанию массы
    public IActionResult OrderByMass() => View();


    // выборка по массе в выбранном диапазоне
    public IActionResult SelectByMassRange() => View(new RangeBindingModel { Min = 0, Max = 0 });

    [HttpPost]
    public IActionResult SelectByMassRange(RangeBindingModel model)
    {
        if (model.Min > model.Max)
            ModelState.AddModelError("Min", "Минимальное значение не может быть больше максимального!");

        return View(model);
    }


    // выборка по родному миру
    public async Task<ViewResult> SelectByHomeWorldAsync()
    {
        ViewBag.HomeWorldList = new SelectList((await DataService.GetPlanetNameListAsync())!);

        return View(new StringBindingModel(""));
    }

    [HttpPost]
    public async Task<ViewResult> SelectByHomeWorldAsync(StringBindingModel model)
    {
        ViewBag.HomeWorldList = new SelectList((await DataService.GetPlanetNameListAsync())!);

        return View(new StringBindingModel(model.Value));
    }

    #endregion
}
