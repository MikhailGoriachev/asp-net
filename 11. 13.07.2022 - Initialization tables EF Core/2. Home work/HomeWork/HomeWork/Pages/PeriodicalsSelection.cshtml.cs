using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Pages;

public class PeriodicalsSelection : PageModel
{
    // коллекция записей
    public IQueryable<Periodical>? PeriodicalList { get; set; }

    // конекст базы данных
    public PeriodicalsContext Data { get; set; }

    // название обработчика
    public string? NameHandler { get; set; }
    
    // список категорий (типов издания)
    public SelectList Categories { get; set; }

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

    // выбранная категория (вид издания)
    [BindProperty]
    public int? CategoryId { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public PeriodicalsSelection(PeriodicalsContext data)
    {
        Data = data;

        // заполение списка категорий
        Categories = new SelectList(Data.Categories, "Id", "Name");
    }

    #endregion

    #region Методы

    // все записи
    public void OnGet() =>
        PeriodicalList = Data.Periodicals.AsQueryable().Include(p => p.Category);

    // выборка подписки по индексу издания
    public void OnPostSelectionByIndex() =>
        PeriodicalList = Index == null
            ? Data.Periodicals.AsQueryable().Include(p => p.Category)
            : Data.Periodicals.AsQueryable().Include(p => p.Category).Where(p => p.Index == Index);


    // выборка подписок по диапазона цены
    public void OnPostSelectionByPriceRange() =>
        PeriodicalList = MinPrice == null && MaxPrice == null
            ? Data.Periodicals.AsQueryable().Include(p => p.Category)
            : Data.Periodicals.AsQueryable().Include(p => p.Category).Where(p => p.Price >= MinPrice && p.Price <= MaxPrice);


    // выборка подписок по диапазону длительности подписки
    public void OnPostSelectionByDurationRange() =>
        PeriodicalList = MinDuration == null && MaxDuration == null
            ? Data.Periodicals.AsQueryable().Include(p => p.Category)
            : Data.Periodicals.AsQueryable().Include(p => p.Category).Where(p => p.Duration >= MinDuration && p.Duration <= MaxDuration);


    // выборка подписок по категории (типу издания)
    public void OnPostSelectionByCategory() =>
        PeriodicalList = CategoryId == null
            ? Data.Periodicals.AsQueryable().Include(p => p.Category)
            : Data.Periodicals.AsQueryable().Include(p => p.Category).Where(p => p.CategoryId == CategoryId);


    #endregion
}
