using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Home_work.Infrastructure;
using Newtonsoft.Json;
using System.IO;
using Home_work.Models.Planets;
using Home_work.ViewModels;

namespace Home_work.Controllers;


[ServiceFilter(typeof(ActionsFilterAttribute))]
public class PlanetsController : Controller
{
    //Коллекция людей
    public IActionResult Default()
    {
        return View("Planets");
    }

    //Подробная информация о планете
    public IActionResult PlanetInfo(string value)
        => View("DetailsPlanet", value);

    //Выборка по типу поверхности
    public IActionResult SelectBySurface(string Surface)
        => View((object)Surface);

    //Выборка по диапазону диаметра планеты
    public IActionResult SelectByDiameterRange()
    {

        return View("RangeForm",new RangeModel(9_000,16_000,"Planets", "SelectByDiameterRange"));
    }

    [HttpPost]
    public IActionResult SelectByDiameterRange(RangeModel model)
    {

        if (!ModelState.IsValid)
            return View("RangeForm", model);
        else if(model.Min >= model.Max)
        {
            ModelState.AddModelError(nameof(model.Min), "Минимальное значение должно быть < Максимального");
            ModelState.AddModelError(nameof(model.Max), "Максимальное значение должно быть > Минимального");
            return View("RangeForm", model);
        }

        return View(model);
    }

    //Сортировка по убванию массы планеты
    public IActionResult OrderByMass()
        => View("OrderByPlanetMass");


}
