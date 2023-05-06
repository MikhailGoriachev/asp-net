using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = HomeWork.Models.Route;

namespace HomeWork.Controllers;

public class TouristAgencyController : Controller
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирущий
    public TouristAgencyController(TouristAgencyContext dataContext) =>
        DataContext = dataContext;

    #endregion

    #region Методы действий

    #region CRUD

    // добавление, редактирование данных о клиенте

    // добавление клиента
    public IActionResult AddClient()
    {
        ViewBag.IsAdd = true;

        return View("ClientForm", new Client());
    }

    [HttpPost]
    public async Task<IActionResult> AddClient(Client client)
    {
        ViewBag.IsAdd = true;

        if (!ModelState.IsValid)
            return View("ClientForm", client);

        DataContext.Clients!.Add(client);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Clients");
    }


    // редактирование клиента
    public async Task<IActionResult> EditClient(int id)
    {
        ViewBag.IsAdd = false;

        var client = await DataContext.Clients?.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id)!;

        return client != null ? View("ClientForm", client) : RedirectToAction("Clients");
    }

    [HttpPost]
    public async Task<IActionResult> EditClient(Client client)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;
            return View("ClientForm", client);
        }

        DataContext.Clients!.Update(client);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Clients");
    }

    // добавление маршрута
    public IActionResult AddRoute()
    {
        ViewBag.IsAdd = true;

        ViewBag.CountryList = new SelectList(DataContext.Countries, "Id", "Name");
        ViewBag.ObjectiveList = new SelectList(DataContext.Countries, "Id", "Name");

        return View("RouteForm", new Route());
    }

    [HttpPost]
    public async Task<IActionResult> AddRoute(Route route)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;

            ViewBag.CountryList = new SelectList(DataContext.Countries, "Id", "Name");
            ViewBag.ObjectiveList = new SelectList(DataContext.Countries, "Id", "Name");

            return View("RouteForm", route);
        }

        DataContext.Routes!.Add(route);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Routes");
    }

    // редактирование маршрута
    public async Task<IActionResult> EditRoute(int id)
    {
        ViewBag.IsAdd = false;

        var route = await DataContext.Routes?.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id)!;

        ViewBag.CountryList = new SelectList(DataContext.Countries, "Id", "Name");
        ViewBag.ObjectiveList = new SelectList(DataContext.Objectives, "Id", "Name");

        return route != null ? View("RouteForm", route) : RedirectToAction("Routes");
    }

    [HttpPost]
    public async Task<IActionResult> EditRoute(Route route)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;

            ViewBag.CountryList = new SelectList(DataContext.Countries, "Id", "Name");
            ViewBag.ObjectiveList = new SelectList(DataContext.Objectives, "Id", "Name");

            return View("RouteForm", route);
        }

        DataContext.Routes!.Update(route);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Routes");
    }


    // добавление поездки
    public IActionResult AddVisit()
    {
        ViewBag.IsAdd = true;

        ViewBag.ClientList = new SelectList(DataContext.Clients, "Id", "FullDataClient");
        ViewBag.RouteList = new SelectList(DataContext.Routes!.AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Objective), "Id", "FullDataRoute");


        return View("VisitForm", new Visit());
    }

    [HttpPost]
    public async Task<IActionResult> AddVisit(Visit visit)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;

            ViewBag.ClientList = new SelectList(DataContext.Clients, "Id", "FullDataClient");
            ViewBag.RouteList = new SelectList(DataContext.Routes!.AsNoTracking()
                .Include(r => r.Country)
                .Include(r => r.Objective), "Id", "FullDataRoute");


            return View("VisitForm", visit);
        }

        DataContext.Visits!.Add(visit);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Visits");
    }

    // редактирование поездки
    public async Task<IActionResult> EditVisitAsync(int id)
    {
        ViewBag.IsAdd = false;

        var visit = await DataContext.Visits?.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id)!;

        ViewBag.ClientList = new SelectList(DataContext.Clients!.AsNoTracking(), "Id", "FullDataClient");
        ViewBag.RouteList = new SelectList(DataContext.Routes!.AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Objective), "Id", "FullDataRoute");

        return visit != null ? View("VisitForm", visit) : RedirectToAction("Visits");
    }

    [HttpPost]
    public async Task<IActionResult> EditVisit(Visit visit)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;

            ViewBag.ClientList = new SelectList(DataContext.Clients, "Id", "FullDataClient");
            ViewBag.RouteList = new SelectList(DataContext.Routes!.AsNoTracking()
                .Include(r => r.Country)
                .Include(r => r.Objective), "Id", "FullDataRoute");


            return View("VisitForm", visit);
        }

        DataContext.Visits!.Update(visit);
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Visits");
    }


    // удаление поездки
    public async Task<IActionResult> RemoveVisit(int id)
    {
        DataContext.Entry(new Visit { Id = id }).State = EntityState.Deleted;
        await DataContext.SaveChangesAsync();

        return RedirectToAction("Visits");
    }

    #endregion


    // вывод записей таблицы "Клиенты"
    public async Task<IActionResult> Clients() => View(await DataContext.Clients!.AsNoTracking().ToListAsync());


    // вывод записей таблицы "Страны"
    public async Task<IActionResult> Countries() => View(await DataContext.Countries!.AsNoTracking().ToListAsync());


    // вывод записей таблицы "Цели поездки"
    public async Task<IActionResult> Objectives() => View(await DataContext.Objectives!.AsNoTracking().ToListAsync());


    // вывод записей таблицы "Маршруты"
    public async Task<IActionResult> Routes() =>
        View(await DataContext.Routes!.AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Objective)
            .ToListAsync()
        );


    // вывод записей таблицы "Поездки"
    public async Task<IActionResult> Visits() => View(await DataContext.Visits!.AsNoTracking()
        .Include(v => v.Client)
        .Include(v => v.Route)
        .ThenInclude(r => r!.Country)
        .Include(v => v.Route!.Objective)
        .ToListAsync()
    );


    // сортировка поездок
    [HttpGet("[controller]/[action]/{sortField}/{sortState?}")]
    public async Task<IActionResult> OrderByVisits(string sortField, SortStateVisits sortState = SortStateVisits.CountryAsc)
    {
        // коллекция поездок
        var visits = DataContext.Visits!.AsNoTracking()
            .Include(v => v.Client)
            .Include(v => v.Route)
            .ThenInclude(r => r!.Country)
            .Include(v => v.Route!.Objective)
            .AsQueryable();

        // установка названия поля сортировки
        ViewBag.SortField = sortField;

        ViewBag.DateStartSort = sortState == SortStateVisits.DateStartAsc
            ? SortStateVisits.DateStartDesc
            : SortStateVisits.DateStartAsc;
        ViewBag.DurationSort = sortState == SortStateVisits.DurationAsc
            ? SortStateVisits.DurationDesc
            : SortStateVisits.DurationAsc;
        ViewBag.CountrySort = sortState == SortStateVisits.CountryAsc
            ? SortStateVisits.CountryDesc
            : SortStateVisits.CountryAsc;

        // сортировка
        visits = sortState switch
        {
            // по дате начала поездки
            SortStateVisits.DateStartAsc => visits.OrderBy(v => v.DateStart.Date),
            SortStateVisits.DateStartDesc => visits.OrderByDescending(v => v.DateStart.Date),

            // по длительности
            SortStateVisits.DurationAsc => visits.OrderBy(v => v.Duration),
            SortStateVisits.DurationDesc => visits.OrderByDescending(v => v.Duration),

            // по стране пребывания
            SortStateVisits.CountryAsc => visits.OrderBy(v => v.Route!.Country!.Name),
            SortStateVisits.CountryDesc => visits.OrderByDescending(v => v.Route!.Country!.Name),

            _ => visits.OrderBy(v => v.Route!.Country!.Name)
        };

        return View("Visits", await visits.ToListAsync());
    }

    #endregion
}
