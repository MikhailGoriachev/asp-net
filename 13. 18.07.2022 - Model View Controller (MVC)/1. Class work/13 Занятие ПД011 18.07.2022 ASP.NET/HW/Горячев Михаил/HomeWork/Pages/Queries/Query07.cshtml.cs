using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Queries;

// Запрос 7. Выполняет группировку по полю Цель поездки. Определяет минимальную, среднюю и максимальную
// стоимость 1 дня пребывания
public class Query07 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Query07Result> Results { get; set; } = null!;

    #region Конструкторы

    // конструктор инициализирующий
    public Query07(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() => Results = DataContext.Objectives!.AsNoTracking()
        .Include(o => o.Routes)
        .Select(o => new Query07Result(
            o,
            o.Routes!.Min(r => r.DailyCost),
            o.Routes!.Average(r => r.DailyCost),
            o.Routes!.Max(r => r.DailyCost),
            o.Routes!.Count));

    #endregion
}

// результат запроса
public record Query07Result(Objective Objective, int? MinDailyCost, double? AvgDailyCost, int? MaxDailyCost, int? Count);
