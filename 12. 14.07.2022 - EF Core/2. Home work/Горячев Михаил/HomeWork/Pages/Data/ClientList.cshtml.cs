using HomeWork.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages.Data;

public class ClientList : PageModel
{
    // записи
    public IQueryable<Client> Clients { get; set; } = null!;
    
    // контекст базы данных
    public TouristAgencyContext? DataContext { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public ClientList(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet()
    {
        Clients = DataContext!.Clients!;
    }

    #endregion
}
