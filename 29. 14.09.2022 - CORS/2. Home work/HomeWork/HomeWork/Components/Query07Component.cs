using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Components;

// Компонент для запроса 7
// Выполняет группировку по полю Цель поездки.  Определяет минимальную, среднюю и максимальную стоимость 1 дня
// пребывания
public class Query07Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query07Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync() =>
        View(await DataContext.Objectives!.AsNoTracking()
            .Include(o => o.Routes)
            .Select(o => new Query07Result(
                    o,
                    o.Routes!.Min(r => r.DailyCost),
                    o.Routes!.Average(r => r.DailyCost),
                    o.Routes!.Max(r => r.DailyCost),
                    o.Routes!.Count)
            )
            .ToListAsync()
        );
}
