using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Query3 : PageModel
{
    private TravelAgencyContext _dbContext;
    
    // Коллекция данных для отображения
    public IQueryable<Client>? DisplayClients { get; private set; }
    
    // Список стран
    public SelectList CountriesList;
    
    [BindProperty] public int Days { get; set; }
    [BindProperty] public int CountryId { get; set; }
    
    public Query3(TravelAgencyContext agencyContext)
    {
        _dbContext = agencyContext;
        CountriesList = new SelectList(_dbContext.Countries.AsNoTracking(), "Id", "Name");
    }

    public void OnGet() => DisplayClients = _dbContext.Clients.AsNoTracking();

    public void OnPost() =>
        DisplayClients = _dbContext.Travels
            .AsNoTracking()
            .Where(tr => tr.Route!.CountryId == CountryId && tr.Duration == Days)
            .Select(tr => tr.Client)!;
}