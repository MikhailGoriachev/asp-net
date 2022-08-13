using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages;

public class PeriodicalsSelection : PageModel
{
    // коллекция записей
    public List<Periodical>? PeriodicalList { get; set; }

    // конекст базы данных
    public PeriodicalsContext Data { get; set; }

    // название обработчика
    public string? NameHandler { get; set; }

    // индекc
    [BindProperty]
    public string? Index { get; set; }

    // минимальное значение цены
    [BindProperty]
    public int? MinPrice { get; set; }

    // максимальное значение цены
    [BindProperty]
    public int? MaxPrice { get; set; }

    // минимальное значение длительности
    [BindProperty]
    public int? MinDuration { get; set; }

    // максимальное значение длительности
    [BindProperty]
    public int? MaxDuration { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public PeriodicalsSelection(PeriodicalsContext data)
    {
        Data = data;
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() =>
        PeriodicalList = Data.Periodicals.AsQueryable().ToList();

    // выборка подписки по индексу издания
    public void OnPostSelectionByIndex() =>
        PeriodicalList = Index == null
            ? Data.Periodicals.AsQueryable().ToList()
            : Data.Periodicals.Where(p => p.Index == Index).ToList();


    // выборка подписок по диапазона цены
    public void OnPostSelectionByPriceRange() =>
        PeriodicalList = MinPrice == null && MaxPrice == null
            ? Data.Periodicals.AsQueryable().ToList()
            : Data.Periodicals.Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).ToList();


    // выборка подписок по диапазону длительности подписки
    public void OnPostSelectionByDurationRange() =>
        PeriodicalList = MinDuration == null && MaxDuration == null
            ? Data.Periodicals.AsQueryable().ToList()
            : Data.Periodicals.Where(p => p.Duration >= MinDuration && p.Duration <= MaxDuration).ToList();


    #endregion
}
