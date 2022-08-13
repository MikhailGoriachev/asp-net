using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Forms;

public class ClientForm : PageModel
{
    // запись
    [BindProperty]
    public Client? ClientItem { get; set; }

    // контекст базы данных
    public TouristAgencyContext DataContext { get; set; }

    // статус формы
    [BindProperty]
    public bool IsAdd { get; set; } = true;

    // сообщение об ошибке
    public string? ErrorMessage { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public ClientForm(TouristAgencyContext dataContext)
    {
        // установка контекста
        DataContext = dataContext;
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
        ClientItem = DataContext.Clients!.FirstOrDefault(p => p.Id == id);
    }

    // сохранить изменения
    public async Task<IActionResult> OnPostSaveAsync()
    {
        try
        {
            DataContext.Update(ClientItem!);
            await DataContext.SaveChangesAsync();
            return RedirectToPage("/Data/ClientList");
        }
        catch (Exception e)
        {
            ErrorMessage = $"Паспортные данные должны быть уникальны!";
            return Page();
        }
    }

    #endregion

}
