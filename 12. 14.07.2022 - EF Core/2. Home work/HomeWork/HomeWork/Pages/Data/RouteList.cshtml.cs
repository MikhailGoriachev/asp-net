using HomeWork.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Route = HomeWork.Models.Route;

namespace HomeWork.Pages.Data;

public class RouteList : PageModel
{
    // записи
    public IQueryable<Route> Routes { get; set; } = null!;

    // контекст базы данных
    public TouristAgencyContext? DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public RouteList(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet()
    {
        Routes = DataContext!.Routes!
            .AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Objective);
    }

    #endregion
}
