using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages;

public class Categories : PageModel
{
    // контекст базы данных
    public PeriodicalsContext Data { get; set; }

    // записи
    public IQueryable<Category> CategoryList { get; set; } = null!;

    #region Конструкторы

    // конструктор инициализирующий
    public Categories(PeriodicalsContext data)
    {
        Data = data;
    }

    #endregion

    #region Методы

    // вывод всех записей
    public void OnGet()
    {
        CategoryList = Data.Categories.AsNoTracking();
    }

    // удаление записи
    public IActionResult OnGetRemoveAt(int id)
    {
        Data.Remove(Data.Categories.FirstOrDefault(p => p.Id == id)!);
        Data.SaveChanges();
        return RedirectToPage("Categories");
    }

    #endregion

}
