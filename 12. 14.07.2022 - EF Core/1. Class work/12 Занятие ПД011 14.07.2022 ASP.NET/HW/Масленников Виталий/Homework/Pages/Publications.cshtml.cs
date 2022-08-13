using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Publications : PageModel
{
    private PeriodicalsContext _dbContext;

    // Коллекция данных для отображения
    public IQueryable<Publication>? DisplayPublications { get; private set; }

    // Объект для изменения/добавления данных
    [BindProperty] public Publication Publication { get; set; } = new() { StartDate = DateTime.Today, Duration = 1 };

    // Значения для списка категорий
    public SelectList Categories { get; private set; }
    
    // Значения фильтров
    [BindProperty] public int? Category { get; set; }
    [BindProperty] public int? PriceFrom { get; set; }
    [BindProperty] public int? PriceTo { get; set; }
    [BindProperty] public int? DurationFrom { get; set; }
    [BindProperty] public int? DurationTo { get; set; }
    [BindProperty] public string? SearchIndex { get; set; }


    public Publications(PeriodicalsContext dbContext)
    {
        _dbContext = dbContext;
        Categories = 
            new SelectList(_dbContext.Categories.AsNoTracking().ToList(), "Id", "CategoryName");
    }

    public void OnGetAsync() => DisplayPublications = _dbContext.Publications;
    public void OnPost() => DisplayPublications = _dbContext.Publications;

    
    public async Task<IActionResult> OnPostAddAsync()
    {
        await _dbContext.Publications.AddAsync(Publication);
        await _dbContext.SaveChangesAsync();
        DisplayPublications = _dbContext.Publications;
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var pub = await _dbContext.Publications.FindAsync(id);

        if (pub != null)
        {
            _dbContext.Publications.Remove(pub);
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToPage();
    }

    // Инициация редактирования
    public async Task OnGetEditAsync(int id)
    {
        DisplayPublications = _dbContext.Publications;
        Publication = (await _dbContext.Publications.FirstOrDefaultAsync(p => p.PublicationId == id))!;
    }

    // Обновление отредактированных данных
    public async Task<IActionResult> OnPostEditAsync()
    {
        _dbContext.Publications.Update(Publication);
        await _dbContext.SaveChangesAsync();

        return RedirectToPage();
    }
    
    public void OnPostFilterCategory() =>
        DisplayPublications = _dbContext.Publications.Where(p => p.CategoryId == Category);
    public void OnPostFindIndexAsync() => 
        DisplayPublications =  _dbContext.Publications.Where(p => p.PubIndex == SearchIndex);
    
    public void OnPostFilterPrice()
    {
        // параметры фильтров не введены => отсутствуют
        if (PriceFrom is null && PriceTo is null)
        {
            DisplayPublications = _dbContext.Publications;
            return;
        }

        // не введён один из порогов => предельное
        PriceFrom ??= int.MinValue;
        PriceTo ??= int.MaxValue;

        DisplayPublications = _dbContext.Publications.Where(p => p.Price >= PriceFrom && p.Price <= PriceTo);
    }

    public void OnPostFilterDuration()
    {
        // параметры фильтров не введены => отсутствуют
        if (DurationFrom is null && DurationTo is null)
        {
            DisplayPublications = _dbContext.Publications;
            return;
        }

        // не введён один из порогов => предельное
        DurationFrom ??= int.MinValue;
        DurationTo ??= int.MaxValue;

        DisplayPublications = _dbContext.Publications
            .Where(p => p.Duration >= DurationFrom && p.Duration <= DurationTo);
    }
}