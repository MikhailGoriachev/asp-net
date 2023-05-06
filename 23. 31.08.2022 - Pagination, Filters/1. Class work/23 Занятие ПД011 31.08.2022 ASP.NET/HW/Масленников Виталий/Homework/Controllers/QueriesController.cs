using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Homework.Controllers;

public class QueriesController : Controller
{
    private TravelAgencyContext _dbContext;

    public QueriesController(TravelAgencyContext agencyContext)
    {
        _dbContext = agencyContext;
    }
    
    // GET 
    public IActionResult Query1()
    {
        // Список целей поездки
        ViewBag.purposesList = new SelectList(_dbContext.Purposes.AsNoTracking(), "Id", "Name");
        return View(_dbContext.Routes
            .Include(r => r.Country)
            .Include(r => r.Purpose)
            .AsNoTracking());
    }
    
    // POST
    [HttpPost]
    public IActionResult Query1(int purposeId)
    {
        // Список целей поездки
        ViewBag.purposesList = new SelectList(_dbContext.Purposes.AsNoTracking(), "Id", "Name");
        return View(_dbContext.Routes
            .Where(p => p.PurposeId == purposeId)
            .Include(r => r.Country)
            .Include(r => r.Purpose)
            .AsNoTracking());
    }
    
    // GET Выбор информации о маршрутах с заданной целью поездки и стоимостью транспортных услуг
    // менее заданного значения
    public IActionResult Query2()
    {
        // Список целей поездки
        ViewBag.purposesList = new SelectList(_dbContext.Purposes.AsNoTracking(), "Id", "Name");
        return View(_dbContext.Routes
            .Include(r => r.Country)
            .Include(r => r.Purpose)
            .AsNoTracking());
    }
    
    // POST Выбор информации о маршрутах с заданной целью поездки и стоимостью транспортных услуг
    // менее заданного значения
    [HttpPost]
    public IActionResult Query2(int PurposeId, int TransferCost)
    {
        // Список целей поездки
        ViewBag.purposesList = new SelectList(_dbContext.Purposes.AsNoTracking(), "Id", "Name");
        
        return View(_dbContext.Routes
            .Where(p => p.PurposeId == PurposeId && p.Country!.TransferCost < TransferCost)
            .Include(r => r.Country)
            .Include(r => r.Purpose)
            .AsNoTracking());
    }
    
    // GET Фильтр по клиентам с поездками в выбранную страну на указанное количество дней
    public IActionResult Query3()
    {
        // Список стран
        ViewBag.countriesList = new SelectList(_dbContext.Countries.AsNoTracking(), "Id", "Name");
        return View(_dbContext.Clients);
    }

    // POST фильтр по клиентам с поездками в выбранную страну на указанное количество дней
    [HttpPost]
    public IActionResult Query3(int CountryId, int Days)
    {
        // Список стран
        ViewBag.countriesList = new SelectList(_dbContext.Countries.AsNoTracking(), "Id", "Name");
        
        return View(_dbContext.Travels.Where(tr => tr.Route!.CountryId == CountryId && tr.Duration == Days)
            .Select(tr => tr.Client));
    }
    
    // GET Фильтр по выбору информации о маршрутах в заданную страну
    public IActionResult Query4()
    {
        // Список стран
        ViewBag.countriesList = new SelectList(_dbContext.Countries.AsNoTracking(), "Id", "Name");
        return View(_dbContext.Routes
            .Include(r => r.Country)
            .Include(r => r.Purpose)
            .AsNoTracking());
    }

    // POST фильтр по выбору информации о маршрутах в заданную страну
    [HttpPost]
    public IActionResult Query4(int CountryId)
    {
        // Список стран
        ViewBag.countriesList = new SelectList(_dbContext.Countries.AsNoTracking(), "Id", "Name");
        
        return View(_dbContext.Routes
            .Where(p => p.CountryId == CountryId)
            .Include(r => r.Country)
            .Include(r => r.Purpose)
            .AsNoTracking());
    }
    
    
    // GET Фильтр по стране с ценой визы в указанном диапазоне
    public IActionResult Query5() => View(_dbContext.Countries.AsNoTracking());

    // POST фильтр по стране с ценой визы в указанном диапазоне
    [HttpPost]
    public IActionResult Query5(int MinPrice, int MaxPrice) =>
        View(_dbContext.Countries
            .Where(c => c.VisaCost >= MinPrice && c.VisaCost <= MaxPrice)
            .AsNoTracking()); 
    
    
    // GET Фильтр - для каждой поездки полная стоимость с НДС
    public IActionResult Query6()
    {
        return View(_dbContext.Travels.Include(tr => tr.Route)
            .ThenInclude(r => r!.Purpose)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Country)
            .ToList()
            .Select(travel => new ResultQuery6(
                travel.Route!.Country!.Name!,
                travel.Route!.Purpose!.Name!,
                travel.StartDate,
                travel.Duration,
                (int)Math.Floor((travel.Duration * (travel.Route!.DailyCost + travel.Route!.Country!.DailyCost)
                                 + travel.Route.Country.TransferCost + travel.Route.Country.VisaCost) * 1.03)
            )).OrderBy(tr => tr.Country)
        );
    }

    // GET Группировка по полю Цель поездки.
    // Определяет минимальную, среднюю и максимальную стоимость 1 дня пребывания
    public IActionResult Query7()
    {
        return View(_dbContext.Travels.AsNoTracking().GroupBy(tr => tr.Route!.Purpose!.Name,
            (key, group) => new ResultQuery7(
                key!,
                group.Min(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost),
                group.Average(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost),
                group.Max(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost)
            )));
    }

    public IActionResult Query8()
    {
        return View(_dbContext.Routes
            .AsNoTracking()
            .GroupBy(route => route.Country!.Name,
                (key, group) => new ResultQuery8(
                    key!,
                    group.Average(g => g.Country!.TransferCost)
                )));
    }
}