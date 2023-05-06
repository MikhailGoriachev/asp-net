using HomeWork.Models;
using HomeWork.Models.BindingModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controllers;

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
    public IActionResult Query01()
    {
        // список целей поездки
        ViewBag.ObjectiveList = new SelectList(
            DataContext.Objectives?.AsNoTracking(),
            "Id",
            "Name"
        );

        // название выбранной цели поездки
        ViewBag.ObjectiveName = "————";

        return View(new IntBindingModel());
    }

    [HttpPost]
    public async Task<IActionResult> Query01Async(IntBindingModel model)
    {
        // список целей поездки
        ViewBag.ObjectiveList = new SelectList(
            DataContext.Objectives?.AsNoTracking(),
            "Id",
            "Name"
        );

        // название выбранной цели поездки
        ViewBag.ObjectiveName = (await DataContext.Objectives!.AsNoTracking()
            .FirstAsync(o => o.Id == model.Value)).Name!;

        return View(model);
    }


    // запрос 2
    // Выбирает информацию о маршрутах с заданной целью поездки и стоимостью транспортных услуг менее заданного значения
    public IActionResult Query02()
    {
        // список целей поездки
        ViewBag.ObjectiveList = new SelectList(
            DataContext.Objectives?.AsNoTracking(),
            "Id",
            "Name"
        );

        // название выбранной цели поездки и максимальная стоимость
        ViewBag.ObjectiveName = "————";
        ViewBag.MaxPrice = "————";

        return View(new Query02BindingModel());
    }

    [HttpPost]
    public async Task<IActionResult> Query02Async(Query02BindingModel model)
    {
        // список целей поездки
        ViewBag.ObjectiveList = new SelectList(
            DataContext.Objectives?.AsNoTracking(),
            "Id",
            "Name"
        );

        // название выбранной цели поездки и максимальная стоимость
        ViewBag.ObjectiveName = (await DataContext.Objectives!.AsNoTracking()
            .FirstAsync(o => o.Id == model.ObjectiveId)).Name!;
        ViewBag.MaxPrice = model.MaxPrice;

        return View(model);
    }


    // запрос 3
    // Выбирает информацию о клиентах, совершивших поездки с заданным количеством дней пребывания в стране
    public IActionResult Query03()
    {
        ViewBag.Duration = "————";

        return View(new IntBindingModel());
    }

    [HttpPost]
    public IActionResult Query03(IntBindingModel model)
    {
        ViewBag.Duration = model.Value;

        return View(model);
    }


    // запрос 4
    // Выбирает информацию о маршрутах в заданную страну
    public IActionResult Query04()
    {
        ViewBag.CountryList = new SelectList(
            DataContext.Countries!.AsNoTracking(),
            "Id",
            "Name"
        );

        ViewBag.CountryName = "————";

        return View(new IntBindingModel());
    }

    [HttpPost]
    public async Task<IActionResult> Query04Async(IntBindingModel model)
    {
        ViewBag.CountryList = new SelectList(
            DataContext.Countries!.AsNoTracking(),
            "Id",
            "Name"
        );

        ViewBag.CountryName = (await DataContext.Countries!.AsNoTracking()
            .FirstAsync(c => c.Id == model.Value)).Name!;

        return View(model);
    }


    // запрос 5
    // Выбирает информацию о странах, для которых стоимость оформления визы есть значение из некоторого диапазона
    public IActionResult Query05()
    {
        ViewBag.MinCost = "————";
        ViewBag.MaxCost = "————";

        return View(new RangeBindingModel());
    }

    [HttpPost]
    public IActionResult Query05(RangeBindingModel model)
    {
        ViewBag.MinCost = model.Min;
        ViewBag.MaxCost = model.Max;

        return View(model);
    }


    // запрос 6
    // Вычисляет для каждой поездки ее полную стоимость с НДС. Включает поля Страна назначения, Цель поездки,
    // Дата начала поездки, Количество дней пребывания, Полная стоимость поездки. Сортировка по полю Страна назначения
    public IActionResult Query06() => View();


    // запрос 7
    // Выполняет группировку по полю Цель поездки.  Определяет минимальную, среднюю и максимальную стоимость 1 дня
    // пребывания
    public IActionResult Query07() => View();


    // запрос 8
    // Выполняет группировку по полю Страна назначения. Для каждой страны вычисляет среднее значение по полю
    // Стоимость транспортных услуг
    public IActionResult Query08() => View();

    #endregion
}
