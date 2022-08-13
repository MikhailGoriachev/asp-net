using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = Homework.Models.Route;

namespace Homework.Pages;
public class Query1 : PageModel
{
    private TravelAgencyContext _dbContext;

    public IQueryable<Route>? DisplayRoutes { get; private set; }
    [BindProperty] public int PurposeId { get; set; }
    
    public Query1(TravelAgencyContext agencyContext)
    {
        _dbContext = agencyContext;
        PurposesList = new SelectList(_dbContext.Purposes.AsNoTracking(), "Id", "Name");
    }

    // Список целей поездки
    public SelectList PurposesList;
    
    public void OnGet() =>
        DisplayRoutes = _dbContext.Routes.AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Purpose);

    public void OnPost() =>
        DisplayRoutes = _dbContext.Routes
            .AsNoTracking()
            .Where(p => p.PurposeId == PurposeId)
            .Include(r => r.Country)
            .Include(r => r.Purpose);
}