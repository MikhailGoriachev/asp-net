using HomeWork.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages.Data;

public class ObjectiveList : PageModel
{
    // записи
    public IQueryable<Objective> Objectives { get; set; } = null!;

    // контекст базы данных
    public TouristAgencyContext? DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public ObjectiveList(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet()
    {
        Objectives = DataContext!.Objectives!;
    }

    #endregion
}
