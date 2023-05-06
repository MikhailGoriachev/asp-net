using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TouristicAgencyMvcCore.Models;
using TouristicAgencyMvcCore.Models.Reports;

namespace TouristicAgencyMvcCore.Controllers;

// Отчеты по заданию - итоговые запросы
public class ReportsController : Controller
{
    // ссылка на базу данных
    private ApplicationContext _context;


    // получение доступа к БД при помощи конструктора
    // с внедрением зависимости
    public ReportsController(ApplicationContext db) {
        _context = db;
    } // ReportsController


    // Отчет 1. Итоговый запрос 1.
    // Выполняет группировку по полю Цель поездки.  Определяет
    // минимальную, среднюю и максимальную стоимость 1 дня пребывания
    public IActionResult Report01() {
        // Формулировка итогового запроса
        ViewBag.Task = "Выполняет группировку по полю \"Цель поездки\". Определяет " +
               "минимальную, среднюю и максимальную стоимость 1 дня пребывания";

        // получить коллекцию записей для отчета
        IQueryable<Result01> rows = _context.Travels
            .Include(t => t.Route)
            .ThenInclude(r => r.Purpose)
            .AsNoTracking()
            .GroupBy(r => r.Route.Purpose.Name,
                (key, group) => new Result01(
                    key,
                    group.Min(t => t.Route.DailyCost),
                    group.Average(t => t.Route.DailyCost),
                    group.Max(t => t.Route.DailyCost),
                    group.Count()
                ));
        return View(rows);
    } // Report01


    // Отчет 2. Итоговый запрос 2
    // Выполняет группировку по полю Страна назначения. Для каждой страны
    // вычисляет среднее значение по полю Стоимость транспортных услуг
    public IActionResult Report02() {
        ViewBag.Task = "Выполняет группировку по полю \"Страна назначения\". Для каждой страны " +
               "вычисляет среднее значение по полю \"Стоимость транспортных услуг\"";

        IQueryable<Result02> rows = _context.Travels
            .Include(t => t.Route)
            .ThenInclude(t => t.Country)
            .AsNoTracking()
            .GroupBy(t => t.Route.Country.Name,
                (key, group) => new Result02(
                    key,
                    group.Average(t => t.Route.Country.TransferCost),
                    group.Count()
                ));

        return View(rows);
    } // Report02

} // class ReportsController

