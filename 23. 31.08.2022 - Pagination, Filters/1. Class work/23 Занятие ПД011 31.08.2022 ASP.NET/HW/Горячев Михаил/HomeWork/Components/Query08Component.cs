using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Components;

// Компонент для запроса 8
// Выполняет группировку по полю Страна назначения. Для каждой страны вычисляет среднее значение по полю
// Стоимость транспортных услуг
public class Query08Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query08Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync() =>
        View(await DataContext.Routes!.AsNoTracking()
            .Include(c => c.Country)
            .GroupBy(r => r.Country!.Name,
                (k, g) =>
                    new Query08Result(
                        k,
                        g.Average(r => r.Country!.CostService),
                        g.Count(r => r.Country != null))
            )
            .ToListAsync()
        );
}
