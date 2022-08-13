using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Forms;

public class VisitForm : PageModel
{
    // запись
    [BindProperty] public Visit? VisitItem { get; set; }

    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }

    // статус формы
    [BindProperty] public bool IsAdd { get; set; } = true;

    // список клиентов
    public SelectList CLients { get; set; }

    // список маршрутов
    public SelectList Routes { get; set; }

    // сообщение об ошибке
    public string? ErrorMessage { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public VisitForm(TouristAgencyContext dataContext)
    {
        // установка контекста
        DataContext = dataContext;

        // список клиентов
        CLients = new SelectList(dataContext.Clients!.AsNoTracking(), "Id", "FullDataClient");

        // список маршрутов
        Routes = new SelectList(dataContext.Routes!
            .Include(r => r.Country)
            .Include(r => r.Objective),
            "Id", "FullDataRoute");
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
        VisitItem = DataContext.Visits!
            .Include(v => v.Client)
            .Include(v => v.Route)
            .Include(v => v.Route!.Country)
            .Include(v => v.Route!.Objective)
            .FirstOrDefault(p => p.Id == id);
    }

    // сохранить изменения
    public async Task<IActionResult> OnPostSaveAsync()
    {
        DataContext.Update(VisitItem!);
        await DataContext.SaveChangesAsync();
        return RedirectToPage("/Data/VisitList");
    }

    #endregion
}
