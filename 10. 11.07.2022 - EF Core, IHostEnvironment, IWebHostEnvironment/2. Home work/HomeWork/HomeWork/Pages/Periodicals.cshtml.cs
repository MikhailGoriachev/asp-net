using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages;

public class Periodicals : PageModel
{
    // контекст базы данных
    public PeriodicalsContext Data { get; set; }

    // записи
    public IQueryable<Periodical> PeriodicalList { get; set; } = null!;

    #region Конструкторы

    // конструктор инициализирующий
    public Periodicals(PeriodicalsContext data)
    {
        Data = data;
    }

    #endregion

    #region Методы

    // вывод всех изданий
    public void OnGet()
    {
        PeriodicalList = Data.Periodicals.AsNoTracking();
    }

    // удаление записи
    public IActionResult OnGetRemoveAt(int id)
    {
        Data.Remove(Data.Periodicals.FirstOrDefault(p => p.Id == id)!);
        Data.SaveChanges();
        return RedirectToPage("Periodicals");
    }

    #endregion

}
