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
    public SelectList TypesEdition { get; set; } = new SelectList(Utils.TypesEditionList);

    #region Конструкторы

    // конструктор инициализирующий
    public PeriodicalForm(PeriodicalsContext data)
    {
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
        PeriodicalItem = Data.Periodicals.FirstOrDefault(p => p.Id == id);
    }

    // сохранить изменения
    public IActionResult OnPostSave()
    {
        Data.Update(PeriodicalItem!);
        Data.SaveChangesAsync();
        return RedirectToPage("Periodicals");
    }

    #endregion
}
