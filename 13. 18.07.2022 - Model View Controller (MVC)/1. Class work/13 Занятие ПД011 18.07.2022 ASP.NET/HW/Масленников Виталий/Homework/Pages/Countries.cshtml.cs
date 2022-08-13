using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Countries : PageModel
{
    private TravelAgencyContext _dbContext;
    public IQueryable<Country>? DisplayCountries { get; private set; }
    
    public Countries(TravelAgencyContext agencyContext) => _dbContext = agencyContext;

    public void OnGet() => DisplayCountries = _dbContext.Countries.AsNoTracking();
}