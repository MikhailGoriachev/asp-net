using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Components;

// Компонент для запроса 4
// Выбирает информацию о маршрутах в заданную страну
public class Query04Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query04Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync(int countryId) =>
        View("~/Views/Shared/_RoutesTable.cshtml", countryId != 0
            ? await DataContext.Routes!.AsNoTracking()
                .Include(r => r.Country)
                .Include(r => r.Objective)
                .Where(r => r.CountryId == countryId)
                .ToListAsync()
            : await DataContext.Routes!.AsNoTracking()
                .Include(r => r.Country)
                .Include(r => r.Objective)
                .ToListAsync()
        );
}
