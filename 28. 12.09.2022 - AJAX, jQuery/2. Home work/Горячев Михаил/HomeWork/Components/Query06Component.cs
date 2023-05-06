using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Components;

// Компонент для запроса 6
// Вычисляет для каждой поездки ее полную стоимость с НДС. Включает поля Страна назначения, Цель поездки,
// Дата начала поездки, Количество дней пребывания, Полная стоимость поездки. Сортировка по полю Страна назначения
public class Query06Component : ViewComponent
{
    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }


    // конструктор инициализирующий
    public Query06Component(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }


    // метод действия
    public async Task<ViewViewComponentResult> InvokeAsync() =>
        View(await DataContext.Visits!.AsNoTracking()
            .Include(v => v.Route)
            .Include(v => v.Route!.Country)
            .Include(v => v.Route!.Objective)
            .Include(v => v.Client)
            .ToListAsync()
        );
}
