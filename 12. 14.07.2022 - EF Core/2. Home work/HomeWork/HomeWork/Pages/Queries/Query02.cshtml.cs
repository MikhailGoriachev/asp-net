using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = HomeWork.Models.Route;

namespace HomeWork.Pages.Queries;

// Запрос 2. Выбирает информацию о маршрутах с заданной целью поездки и стоимостью транспортных услуг
// менее заданного значения
public class Query02 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Route> Routes { get; set; } = null!;

    // список целей поездки
    public SelectList Objectives { get; set; }

    // название списка
    public string TitleList { get; set; } = "Все маршруты";

    // параметры запроса
    [BindProperty] public Query02Params Params { get; set; } = null!;

    #region Конструкторы

    // конструктор инициализирующий
    public Query02(TouristAgencyContext dataContext)
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
    // Выбирает информацию о маршрутах с заданной целью поездки и стоимостью транспортных услуг
    // менее заданного значения
    public void OnPostSelection()
    {
        // выбранная цель поездки
        Objective objective = DataContext.Objectives!.AsNoTracking().FirstOrDefault(o => o.Id == Params.IdObjective)!;

        TitleList = $"Маршруты с целью поездки \"{objective.Name}\" и ценой менее {Params.MaxPrice:n2} ₽";

        Routes = DataContext.Routes!.AsNoTracking()
            .Where(r => r.ObjectiveId == Params.IdObjective && r.Country!.CostService < Params.MaxPrice)
            .Include(r => r.Objective)
            .Include(r => r.Country);
    }

    #endregion
}

// параметры запроса
public record Query02Params(int IdObjective, int MaxPrice);
