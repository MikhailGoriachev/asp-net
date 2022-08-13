using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages;
public class Report01Model : PageModel
{
    // ссылка на базу данных
    private ApplicationContext _context;

    // ссылка на коллекцию сущностей - таблицу из БД
    public IQueryable<Result01> Rows { get; private set; } = null!;

    // формулировка запроса
    public string? Task { get; private set; }


    // получение доступа к БД при помощи конструктора
    // с внедрением зависимости
    public Report01Model(ApplicationContext db) {
        _context = db;
    } // Report01Model


    // Выполняет группировку по полю Цель поездки.  Определяет
    // минимальную, среднюю и максимальную стоимость 1 дня пребывания
    public void OnGet() {
        Task = "Выполняет группировку по полю \"Цель поездки\". Определяет " +
               "минимальную, среднюю и максимальную стоимость 1 дня пребывания";

        Rows = _context.Travels
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
    } // OnGet
} // class Report01Model

// Тип записи коллекции итогового запроса - позиционный record
public record Result01(string Purpose, int MinCost, double AvgCost, int MaxCost, int Amount);

