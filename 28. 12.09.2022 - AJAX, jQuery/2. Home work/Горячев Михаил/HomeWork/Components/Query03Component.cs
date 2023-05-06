using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Components;

// Компонент для запроса 3
// Выбирает информацию о клиентах, совершивших поездки с заданным количеством дней пребывания в стране
public class Query03Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query03Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync(int duration) =>
        View("~/Views/Shared/_ClientsTable.cshtml", duration != 0
            ? DataContext.Clients!.AsNoTracking()
                .Include(c => c.Visits).ToArray()
                .Where(c => c.Visits!.Exists(v => v.Duration == duration))
                .ToList()
            : await DataContext.Clients!.AsNoTracking()
                .Include(c => c.Visits)
                .ToListAsync()
        );
}
