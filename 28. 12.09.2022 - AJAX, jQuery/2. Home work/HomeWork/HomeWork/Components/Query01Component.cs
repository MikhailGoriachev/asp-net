using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Route = HomeWork.Models.Route;

namespace HomeWork.Components;

// Компонент для запроса 1
// Выбирает информацию о маршрутах с заданной целью поездки
public class Query01Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query01Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync(int objectiveId) =>
        View("~/Views/Shared/_RoutesTable.cshtml", objectiveId != 0
            ? await DataContext.Routes!.AsNoTracking()
                .Where(r => r.ObjectiveId == objectiveId)
                .Include(r => r.Country)
                .Include(r => r.Objective)
                .ToListAsync()
            : await DataContext.Routes!.AsNoTracking()
                .Include(r => r.Country)
                .Include(r => r.Objective)
                .ToListAsync()
        );
}
