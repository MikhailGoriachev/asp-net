using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages;

public class Report02Model : PageModel
{
    // ссылка на базу данных
    private ApplicationContext _context;

    // ссылка на коллекцию сущностей - таблицу из БД
    public IQueryable<Result02> Rows { get; private set; } = null!;

    // формулировка запроса
    public string? Task { get; private set; }


    // получение доступа к БД при помощи конструктора
    // с внедрением зависимости
    public Report02Model(ApplicationContext db)
    {
        _context = db;
    } // Report02Model


    // Выполняет группировку по полю Страна назначения. Для каждой страны
    // вычисляет среднее значение по полю Стоимость транспортных услуг
    public void OnGet()
    {
        Task = "Выполняет группировку по полю \"Страна назначения\". Для каждой страны " +
               "вычисляет среднее значение по полю \"Стоимость транспортных услуг\"";

        Rows = _context.Travels
            .Include(t=> t.Route)
            .ThenInclude(t=> t.Country)
            .AsNoTracking()
            .GroupBy(t => t.Route.Country.Name,
                (key, group) => new Result02(
                    key,
                    group.Average(t => t.Route.Country.TransferCost),
                    group.Count()
                ));
    } // OnGet
} // class Report02Model

// Тип записи коллекции итогового запроса
public record Result02(string Country, double AvgCost, int Amount);

