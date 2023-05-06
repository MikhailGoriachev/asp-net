using HomeWork.Models;
using HomeWork.Models.BindingModels;
using HomeWork.Models.ViewModels;
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

    // фильтр по 1, 2, 3 запросам
    public async Task<IActionResult> Filter(FilterByRoutesViewModel? model)
    {
        var objectives = await DataContext.Objectives?.AsNoTracking().ToListAsync()!;
        objectives.Insert(0, new Objective(0, "Все"));

        // список целей поездки
        ViewBag.ObjectiveList = new SelectList(
            objectives,
            "Id",
            "Name"
        );

        var countries = await DataContext.Countries?.AsNoTracking().ToListAsync()!;
        countries.Insert(0, new Country { Id = 0, Name = "Все" });

        // список стран
        ViewBag.CountryList = new SelectList(
            countries,
            "Id",
            "Name"
        );


        // коллекция данных
        var items = DataContext.Routes!.AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Objective)
            .AsQueryable();

        ViewBag.ObjectiveName = "————";
        ViewBag.CountryName = "————";
        ViewBag.MaxPrice = "————";


        if (model == null)
            return View(new FilterByRoutesViewModel(await items.ToListAsync(), 0, 0, null));

        // цель поездки
        if (model.ObjectiveId != 0)
        {
            items = items.Where(i => i.ObjectiveId == model.ObjectiveId);
            ViewBag.ObjectiveName = (await DataContext.Objectives!.AsQueryable()
                .FirstOrDefaultAsync(o => o.Id == model.ObjectiveId))!.Name!;
        }

        // страна
        if (model.CountryId != 0)
        {
            items = items.Where(i => i.CountryId == model.CountryId);
            ViewBag.CountryName = (await DataContext.Countries!.AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == model.CountryId))!.Name!;
        }

        // максимальная цена
        if (model.MaxPrice != null)
        {
            items = items.Where(i => i.Country!.CostService < model.MaxPrice);
            ViewBag.MaxPrice = model.MaxPrice;
        }

        model.Routes = items;

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
