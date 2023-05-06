using HomeWork.Models;
using HomeWork.Models.BindingModels;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class QueriesController : Controller
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирущий
    public QueriesController(TouristAgencyContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действий

    // запрос 1
    // Выбирает информацию о маршрутах с заданной целью поездки
    [HttpPut]
    public async Task<IActionResult> Query01Async([FromForm]IntBindingModel model)
    {
        // список целей поездки
        ViewBag.ObjectiveList = new SelectList(
            DataContext.Objectives?.AsNoTracking(),
            "Id",
            "Name"
        );

        // название выбранной цели поездки
        ViewBag.ObjectiveName = model.Value != 0
            ? (await DataContext.Objectives!.AsNoTracking()
                .FirstAsync(o => o.Id == model.Value)).Name!
            : "————";

        return View(model);
    }


    // запрос 2
    // Выбирает информацию о маршрутах с заданной целью поездки и стоимостью транспортных услуг менее заданного значения
    [HttpPut]
    public async Task<IActionResult> Query02Async([FromForm]Query02BindingModel model)
    {
        // список целей поездки
        ViewBag.ObjectiveList = new SelectList(
            DataContext.Objectives?.AsNoTracking(),
            "Id",
            "Name"
        );

        // название выбранной цели поездки и максимальная стоимость
        ViewBag.ObjectiveName = model.ObjectiveId != 0
            ? (await DataContext.Objectives!.AsNoTracking()
                .FirstAsync(o => o.Id == model.ObjectiveId)).Name!
            : "————";
        ViewBag.MaxPrice = model.ObjectiveId != 0 ? model.MaxPrice.ToString() : "————";

        return View(model);
    }


    // запрос 3
    // Выбирает информацию о клиентах, совершивших поездки с заданным количеством дней пребывания в стране
    [HttpPut]
    public IActionResult Query03([FromForm]IntBindingModel model)
    {
        ViewBag.Duration = model.Value != 0 ? model.Value.ToString() : "————";

        return View(model);
    }


    // запрос 4
    // Выбирает информацию о маршрутах в заданную страну
    [HttpPut]
    public async Task<IActionResult> Query04Async([FromForm]IntBindingModel model)
    {
        ViewBag.CountryList = new SelectList(
            DataContext.Countries!.AsNoTracking(),
            "Id",
            "Name"
        );

        ViewBag.CountryName = model.Value != 0
            ? (await DataContext.Countries!.AsNoTracking()
                .FirstAsync(c => c.Id == model.Value)).Name!
            : "————";

        return View(model);
    }


    // запрос 5
    // Выбирает информацию о странах, для которых стоимость оформления визы есть значение из некоторого диапазона
    [HttpPut]
    public IActionResult Query05([FromForm]RangeBindingModel model)
    {
        ViewBag.MaxCost = model.Max != 0 ? model.Max.ToString() : "————";
        ViewBag.MinCost = model.Max != 0 ? model.Min.ToString() : "————";

        return View(model);
    }


    // запрос 6
    // Вычисляет для каждой поездки ее полную стоимость с НДС. Включает поля Страна назначения, Цель поездки,
    // Дата начала поездки, Количество дней пребывания, Полная стоимость поездки. Сортировка по полю Страна назначения
    [HttpPut]
    public IActionResult Query06() => View();


    // запрос 7
    // Выполняет группировку по полю Цель поездки.  Определяет минимальную, среднюю и максимальную стоимость 1 дня
    // пребывания
    [HttpPut]
    public IActionResult Query07() => View();


    // запрос 8
    // Выполняет группировку по полю Страна назначения. Для каждой страны вычисляет среднее значение по полю
    // Стоимость транспортных услуг
    [HttpPut]
    public IActionResult Query08() => View();

    #endregion
}
