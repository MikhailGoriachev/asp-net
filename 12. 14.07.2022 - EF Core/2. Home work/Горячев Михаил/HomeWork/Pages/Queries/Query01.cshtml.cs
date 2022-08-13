using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = HomeWork.Models.Route;

namespace HomeWork.Pages.Queries;

// Запрос 1. Выбирает информацию о маршрутах с заданной целью поездки
public class Query01 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Route> Routes { get; set; } = null!;

    // список целей поездки
    public SelectList Objectives { get; set; }

    // название списка
    public string TitleList { get; set; } = "Все маршруты";

    // выбранная цель поездки
    [BindProperty] public int? IdObjective { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public Query01(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;

        Objectives = new SelectList(DataContext.Objectives!.AsNoTracking(), "Id", "Name");
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet()
    {
        Routes = DataContext.Routes!.AsNoTracking()
            .Include(r => r.Objective)
            .Include(r => r.Country);
    }


    // выборка записей
    // выбирает информацию о маршрутах с заданной целью поездки
    public void OnPostSelection()
    {
        // выбранная цель поездки
        Objective objective = DataContext.Objectives!.AsNoTracking().FirstOrDefault(o => o.Id == IdObjective)!;

        TitleList = $"Маршруты с целью поездки \"{objective.Name}\"";

        Routes = DataContext.Routes!.AsNoTracking()
            .Where(r => r.ObjectiveId == IdObjective)
            .Include(r => r.Objective)
            .Include(r => r.Country);
    }

    #endregion
}
