using HomeWork.Models;
using HomeWork.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.CompilerServices;
using Utils = HomeWork.Infrastructure.Utils;

namespace HomeWork.Pages;

public class CategoryForm : PageModel
{
    // запись
    [BindProperty]
    public Category? CategoryItem { get; set; }

    // контекст базы данных
    public PeriodicalsContext Data { get; set; }

    // статус формы
    public bool IsAdd { get; set; } = true;

    #region Конструкторы

    // конструктор инициализирующий
    public CategoryForm(PeriodicalsContext data)
    {
        // установка контекста
        Data = data;
    }

    #endregion

    #region Методы

    // добавить запись
    public void OnGetAdd()
    {

    }

    // изменить запись
    public void OnGetEdit(int id)
    {
        IsAdd = false;
        CategoryItem = Data.Categories.FirstOrDefault(p => p.Id == id);
    }

    // сохранить изменения
    public async Task<IActionResult> OnPostSaveAsync()
    {
        Data.Update(CategoryItem!);
        await Data.SaveChangesAsync();
        return RedirectToPage("Categories");
    }

    #endregion
}
