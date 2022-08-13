using HomeWork.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages.Data;

public class CountryList : PageModel
{
    // записи
    public IQueryable<Country> Countries { get; set; } = null!;

    // контекст базы данных
    public TouristAgencyContext? DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public CountryList(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet()
    {
        Countries = DataContext!.Countries!;
    }

    #endregion
}
