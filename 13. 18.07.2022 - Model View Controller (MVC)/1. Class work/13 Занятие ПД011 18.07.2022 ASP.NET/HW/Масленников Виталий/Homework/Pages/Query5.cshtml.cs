using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = Homework.Models.Route;

namespace Homework.Pages;

public class Query5 : PageModel
{
    private TravelAgencyContext _dbContext;
    public IQueryable<Country>? DisplayCountries { get; private set; }

    [BindProperty] public int MinPrice { get; set; }
    [BindProperty] public int MaxPrice { get; set; }
    
    public Query5(TravelAgencyContext agencyContext) => _dbContext = agencyContext;

    public void OnGet() => DisplayCountries = _dbContext.Countries.AsNoTracking();

    public void OnPost() => DisplayCountries = _dbContext.Countries
        .AsNoTracking()
        .Where(c => c.VisaCost >= MinPrice && c.VisaCost <= MaxPrice);
}