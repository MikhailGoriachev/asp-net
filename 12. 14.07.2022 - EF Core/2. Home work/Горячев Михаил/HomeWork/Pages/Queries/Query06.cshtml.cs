using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Queries;

// Запрос 6. Вычисляет для каждой поездки ее полную стоимость с НДС. Включает поля Страна назначения, Цель поездки,
// Дата начала поездки, Количество дней пребывания, Полная стоимость поездки. Сортировка по полю Страна назначения
public class Query06 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Visit> Visits { get; set; } = null!;

    #region Конструкторы

    // конструктор инициализирующий
    public Query06(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() => Visits = DataContext.Visits!.AsNoTracking()
        .Include(v => v.Route)
        .Include(v => v.Route!.Country)
        .Include(v => v.Route!.Objective)
        .Include(v => v.Client);

    #endregion
}
