using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Components;

// Компонент для запроса 5
// Выбирает информацию о странах, для которых стоимость оформления визы есть значение из некоторого диапазона
public class Query05Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query05Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync(int minCost, int maxCost) =>
        View("~/Views/Shared/_CountriesTable.cshtml", maxCost != 0
            ? await DataContext.Countries!.AsNoTracking()
                .Where(c => c.CostVisa >= minCost && c.CostVisa <= maxCost)
                .ToListAsync()
            : await DataContext.Countries!.AsNoTracking().ToListAsync()
        );
}
