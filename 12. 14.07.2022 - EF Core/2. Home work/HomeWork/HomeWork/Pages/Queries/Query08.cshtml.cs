using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Queries;

// Запрос 8. Выполняет группировку по полю Страна назначения. Для каждой страны вычисляет среднее значение по полю
// Стоимость транспортных услуг
public class Query08 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Query08Result> Results { get; set; } = null!;

    #region Конструкторы

    // конструктор инициализирующий
    public Query08(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() => Results = DataContext.Routes!.AsNoTracking()
        .Include(c => c.Country)
        .GroupBy(r => r.Country!.Name,
            (k, g) =>
                new Query08Result(k,
                    g.Average(r => r.Country!.CostService),
                    g.Count(r => r.Country != null)));

    #endregion
}

// результат запроса
public record Query08Result(string CountryName, double? AvgDailyCost, int? Count);
