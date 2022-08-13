using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = HomeWork.Models.Route;

namespace HomeWork.Pages.Queries;

// Запрос 4. Выбирает информацию о маршрутах в заданную страну
public class Query04 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Route> Routes { get; set; } = null!;

    // список стран
    public SelectList Countries { get; set; }

    // название списка
    public string TitleList { get; set; } = "Все маршруты";

    // выбранная страна
    [BindProperty] public int CountryId { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public Query04(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;

        Countries = new SelectList(DataContext.Countries!.AsNoTracking(), "Id", "Name");
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() => Routes = DataContext.Routes!.AsNoTracking()
        .Include(r => r.Country)
        .Include(r => r.Objective);


    // выборка записей
    // Выбирает информацию о маршрутах в заданную страну
    public void OnPostSelection()
    {
        // выбранная страна
        Country country = DataContext!.Countries!.FirstOrDefault(c => c.Id == CountryId)!;

        TitleList = $"Маршруты. Выбранная страна \"{country.Name}\"";

        Routes = DataContext.Routes!.AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Objective)
            .Where(r => r.CountryId == CountryId);
    }

    #endregion
}
