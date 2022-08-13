using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = Homework.Models.Route;

namespace Homework.Pages;

public class Query4 : PageModel
{
    private TravelAgencyContext _dbContext;
    public IQueryable<Route>? DisplayRoutes { get; private set; }

    // Список стран
    public SelectList CountriesList;
    [BindProperty] public int CountryId { get; set; }
    
    public Query4(TravelAgencyContext agencyContext)
    {
        _dbContext = agencyContext;
        CountriesList = new SelectList(_dbContext.Countries.AsNoTracking(), "Id", "Name");
    }

    public void OnGet() =>
        DisplayRoutes = _dbContext.Routes
            .AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Purpose);

    public void OnPost() =>
        DisplayRoutes = _dbContext.Routes
            .AsNoTracking()
            .Where(p => p.CountryId == CountryId)
            .Include(r => r.Country)
            .Include(r => r.Purpose);
}