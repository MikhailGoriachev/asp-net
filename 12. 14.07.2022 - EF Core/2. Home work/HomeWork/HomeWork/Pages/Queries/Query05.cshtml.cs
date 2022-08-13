using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages.Queries;

// Запрос 5. Выбирает информацию о странах, для которых стоимость оформления визы есть значение из некоторого диапазона
public class Query05 : PageModel
{
    // контекст базы даннх
    public TouristAgencyContext DataContext { get; set; }

    // записи для отображения
    public IEnumerable<Country> Countries { get; set; } = null!;

    // название списка
    public string TitleList { get; set; } = "Все страны";

    // параметры запроса
    [BindProperty] public Query05Params Params { get; set; } = null!;

    #region Конструкторы

    // конструктор инициализирующий
    public Query05(TouristAgencyContext dataContext)
    {
        DataContext = dataContext;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() => Countries = DataContext.Countries!.AsNoTracking();


    // выборка записей
    // Выбирает информацию о странах, для которых стоимость оформления визы есть значение из некоторого диапазона
    public void OnPostSelection()
    {
        TitleList = $"Страны. Диапазон стоимости визы \"{Params.MinCost} ₽ - {Params.MaxCost} ₽\"";

        Countries = DataContext.Countries!.AsNoTracking()
            .Where(c => c.CostVisa >= Params.MinCost && c.CostVisa <= Params.MaxCost);
    }

    #endregion
}

// параметры запроса
public record Query05Params(int MinCost, int MaxCost);
