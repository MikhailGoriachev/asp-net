using HomeWork.Models;
using HomeWork.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.CompilerServices;
using Utils = HomeWork.Infrastructure.Utils;

namespace HomeWork.Pages;

public class PeriodicalForm : PageModel
{
    // запись
    [BindProperty]
    public Periodical? PeriodicalItem { get; set; }

    // контекст базы данных
    public PeriodicalsContext Data { get; set; }

    // статус формы
    public bool IsAdd { get; set; } = true;

    // виды изданий
    public SelectList TypesEdition { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public PeriodicalForm(PeriodicalsContext data)
    {
        // установка контекста
        Data = data;

        // заполение списка категорий
        TypesEdition = new SelectList(Data.Categories, "Id", "Name");
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
        PeriodicalItem = Data.Periodicals.FirstOrDefault(p => p.Id == id);
    }

    // сохранить изменения
    public async Task<IActionResult> OnPostSaveAsync()
    {
        Data.Update(PeriodicalItem!);
        await Data.SaveChangesAsync();
        return RedirectToPage("Periodicals");
    }

    #endregion
}
