using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Components;

// Компонент для запроса 2
// Выбирает информацию о маршрутах с заданной целью поездки и стоимостью транспортных услуг менее заданного значения
public class Query02Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query02Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync(int objectiveId, int maxPrice) =>
        View("~/Views/Shared/_RoutesTable.cshtml", objectiveId != 0
            ? await DataContext.Routes!.AsNoTracking()
                .Include(r => r.Country)
                .Where(r => r.ObjectiveId == objectiveId && r.Country!.CostService < maxPrice)
                .Include(r => r.Objective)
                .ToListAsync()
            : await DataContext.Routes!.AsNoTracking()
                .Include(r => r.Country)
                .Include(r => r.Objective)
                .ToListAsync()
        );
}
