using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Homework.Controllers;

[ApiController]
[Route("api/")]
public class QueriesController : Controller
{
    private TravelAgencyContext _dbContext;

    public QueriesController(TravelAgencyContext agencyContext) => _dbContext = agencyContext;

    // PUT Выбор информацию о маршрутах с заданной целью поездки
    [HttpPut("query1")]
    public IActionResult Query1([FromForm]int purposeId)
    {
        ViewBag.Purpose = _dbContext.Purposes.FirstOrDefault(p => p.Id == purposeId)!.Name!;
        
        return View(_dbContext.Routes.AsNoTracking()
            .Where(p => p.PurposeId == purposeId)
            .Include(r => r.Country)
            .Include(r => r.Purpose));
    }

    // PUT Выбор информации о маршрутах с заданной целью поездки и стоимостью транспортных услуг
    // менее заданного значения
    [HttpPut("query2")]
    public IActionResult Query2([FromForm]int purposeId, [FromForm]int transferCost)
    {
        ViewBag.PurposeId = _dbContext.Purposes.FirstOrDefault(p => p.Id == purposeId)!.Name!;
        ViewBag.TransferCost = transferCost;
        
        return View(_dbContext.Routes.AsNoTracking()
            .Where(p => p.PurposeId == purposeId && p.Country!.TransferCost < transferCost)
            .Include(r => r.Country)
            .Include(r => r.Purpose));
    }

    // PUT фильтр по клиентам с поездками в выбранную страну на указанное количество дней
    [HttpPut("query3")]
    public IActionResult Query3([FromForm]int countryId, [FromForm]int days)
    {
        ViewBag.Country = _dbContext.Countries.FirstOrDefault(c => c.Id == countryId)!.Name!;
        ViewBag.Days = days;
        
        return View(_dbContext.Travels.Where(tr => tr.Route!.CountryId == countryId && tr.Duration == days)
            .Select(tr => tr.Client));
    }


    // PUT фильтр по выбору информации о маршрутах в заданную страну
    [HttpPut("query4")]
    public IActionResult Query4([FromForm]int countryId)
    {
        ViewBag.Country = _dbContext.Countries.FirstOrDefault(c => c.Id == countryId)!.Name!;
        
        return View(_dbContext.Routes
            .Where(p => p.CountryId == countryId)
            .Include(r => r.Country)
            .Include(r => r.Purpose)
            .AsNoTracking());
    }

    // PUT фильтр по стране с ценой визы в указанном диапазоне
    [HttpPut("query5")]
    public IActionResult Query5([FromForm]int minPrice, [FromForm]int maxPrice)
    {
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;

        return View(_dbContext.Countries
            .Where(c => c.VisaCost >= minPrice && c.VisaCost <= maxPrice)
            .AsNoTracking());
    }


    // PUT Фильтр - для каждой поездки полная стоимость с НДС
    [HttpPut("query6")]
    public IActionResult Query6() =>
        View(_dbContext.Travels.Include(tr => tr.Route)
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

    // PUT Группировка по полю Цель поездки.
    // Определяет минимальную, среднюю и максимальную стоимость 1 дня пребывания
    [HttpPut("query7")]
    public IActionResult Query7() =>
        View(_dbContext.Travels.AsNoTracking().GroupBy(tr => tr.Route!.Purpose!.Name,
            (key, group) => new ResultQuery7(
                key!,
                group.Min(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost),
                group.Average(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost),
                group.Max(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost)
            )));

    // PUT Выполняет группировку по полю Страна назначения.
    // Для каждой страны вычисляет среднее значение по полю Стоимость транспортных услуг
    [HttpPut("query8")]
    public IActionResult Query8() =>
        View(_dbContext.Routes
            .AsNoTracking()
            .GroupBy(route => route.Country!.Name,
                (key, group) => new ResultQuery8(
                    key!,
                    group.Average(g => g.Country!.TransferCost)
                )));
}