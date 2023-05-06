using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Home_work.Infrastructure;
using Newtonsoft.Json;
using System.IO;
using Home_work.Models;
using Home_work.ViewModels;

namespace Home_work.Controllers;

[ServiceFilter(typeof(ActionsFilterAttribute))]
public class CharactersController : Controller
{
    //Коллекция людей
    public IActionResult Default()
    {
        return View("Characters");
    }

    //Подробная информация о персонаже
    public IActionResult CharacterInfo(string value)
        => View("DetailsCharacter", value);


    //Выборка по диапазону веса
    public IActionResult SelectByWeightRange()
    {

        return View("RangeForm",new RangeModel(30,100, "Characters", "SelectByWeightRange"));
    }

    [HttpPost]
    public IActionResult SelectByWeightRange(RangeModel model)
    {

        if (!ModelState.IsValid)
            return View("RangeForm", model);
        else if(model.Min >= model.Max)
        {
            ModelState.AddModelError(nameof(model.Min), "Минимальное значение должно быть < Максимального");
            ModelState.AddModelError(nameof(model.Max), "Максимальное значение должно быть > Минимального");
            return View("RangeForm", model);
        }

        return View("SelectCharacters",model);
    }

    //Выборка по планетам 
    public IActionResult SelectByPlanet(string Planet)
        =>View("SelectionByPlanet", Planet);

    //Сортировка по массе 
    public IActionResult OrderByWeight()
        =>View();

}
