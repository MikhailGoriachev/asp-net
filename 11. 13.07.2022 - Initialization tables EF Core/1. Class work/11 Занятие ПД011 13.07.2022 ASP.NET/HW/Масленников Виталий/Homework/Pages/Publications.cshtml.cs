using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Publications : PageModel
{
    private PeriodicalsContext _dbContext;

    // Коллекция данных для отображения
    public List<Publication>? DisplayPublications { get; private set; }

    // Объект для изменения/добавления данных
    [BindProperty] public Publication Publication { get; set; } = new() { StartDate = DateTime.Today, Duration = 1 };

    
    // Значения фильтров
    [BindProperty] public int? PriceFrom { get; set; }
    [BindProperty] public int? PriceTo { get; set; }
    [BindProperty] public int? DurationFrom { get; set; }
    [BindProperty] public int? DurationTo { get; set; }
    [BindProperty] public string? SearchIndex { get; set; }


    public Publications(PeriodicalsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task OnGetAsync()
    {
        DisplayPublications = await _dbContext.Publications.AsNoTracking().ToListAsync();
    }

    public async Task OnPostAsync()
    {
        DisplayPublications = await _dbContext.Publications.AsNoTracking().ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        await _dbContext.Publications.AddAsync(Publication);
        await _dbContext.SaveChangesAsync();
        DisplayPublications = await _dbContext.Publications.AsNoTracking().ToListAsync();
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

    public async Task OnGetEditAsync(int id)
    {
        DisplayPublications = await _dbContext.Publications.AsNoTracking().ToListAsync();
        Publication = (await _dbContext.Publications.FirstOrDefaultAsync(p => p.PublicationId == id))!;
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        _dbContext.Publications.Update(Publication);
        await _dbContext.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task OnPostFindIndexAsync()
    {
        DisplayPublications = await _dbContext.Publications
            .AsNoTracking()
            .Where(p => p.PubIndex == SearchIndex)
            .ToListAsync();
    }

    public async Task OnPostFilterPriceAsync()
    {
        // параметры фильтров не введены => отсутствуют
        if (PriceFrom is null && PriceTo is null)
        {
            DisplayPublications = await _dbContext.Publications.AsNoTracking().ToListAsync();
            return;
        }

        // не введён один из порогов => предельное
        PriceFrom ??= int.MinValue;
        PriceTo ??= int.MaxValue;

        DisplayPublications = await _dbContext.Publications.AsNoTracking()
            .Where(p => p.Price >= PriceFrom && p.Price <= PriceTo).ToListAsync();
    }

    public async Task OnPostFilterDurationAsync()
    {
        // параметры фильтров не введены => отсутствуют
        if (DurationFrom is null && DurationTo is null)
        {
            DisplayPublications = await _dbContext.Publications
                .AsNoTracking()
                .ToListAsync();
            return;
        }

        // не введён один из порогов => предельное
        DurationFrom ??= int.MinValue;
        DurationTo ??= int.MaxValue;

        DisplayPublications = await _dbContext.Publications
            .AsNoTracking()
            .Where(p => p.Duration >= DurationFrom && p.Duration <= DurationTo)
            .ToListAsync();
    }
}