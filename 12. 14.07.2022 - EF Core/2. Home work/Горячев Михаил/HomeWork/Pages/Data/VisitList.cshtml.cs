using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Data;

public class VisitList : PageModel
{
    // записи
    public IQueryable<Visit> Visits { get; set; } = null!;

    // контекст базы данных
    public TouristAgencyContext? DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public VisitList(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet()
    {
        Visits = DataContext!.Visits!
            .AsNoTracking()
            .Include(v => v.Client)
            .Include(v => v.Route)
            .Include(v => v.Route!.Country)
            .Include(v => v.Route!.Objective);
    }


    // удаление записи
    public async Task<IActionResult> OnGetRemoveAtAsync(int id)
    {
        DataContext!.Remove(DataContext.Visits!.FirstOrDefault(v => v.Id == id));
        await DataContext.SaveChangesAsync();
        return RedirectToPage("/Data/VisitList");
    }

    #endregion
}
