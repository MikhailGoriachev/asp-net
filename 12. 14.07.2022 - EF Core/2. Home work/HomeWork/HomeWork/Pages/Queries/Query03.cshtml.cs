using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Queries;

// Запрос 3. Выбирает информацию о клиентах, совершивших поездки с заданным количеством дней пребывания в стране
public class Query03 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Client> Clients { get; set; } = null!;

    // название списка
    public string TitleList { get; set; } = "Все клиенты";

    // выбранное количество дней пребывания в стране
    [BindProperty] public int? Duration { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public Query03(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() => Clients = DataContext.Clients!.AsNoTracking();


    // выборка записей
    // Выбирает информацию о клиентах, совершивших поездки с заданным количеством дней пребывания в стране
    public void OnPostSelection()
    {
        TitleList = $"Клиенты. Кол-во дней пребывания \"{Duration}\"";

        Clients = DataContext.Clients!.Include(c => c.Visits).AsNoTracking().ToArray()
            .Where(c => c.Visits!.Exists(v => v.Duration == Duration));
    }

    #endregion
}
