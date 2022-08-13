using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Route = Homework.Models.Route;

namespace Homework.Pages;

public class Query2 : PageModel
{
    private TravelAgencyContext _dbContext;

    public IQueryable<Route>? DisplayRoutes { get; private set; }
    [BindProperty] public int PurposeId { get; set; }
    [BindProperty] public int TransferCost { get; set; }
    
    // Список целей поездки
    public SelectList PurposesList;
    
    public Query2(TravelAgencyContext agencyContext)
    {
        _dbContext = agencyContext;
        PurposesList = new SelectList(_dbContext.Purposes.AsNoTracking(), "Id", "Name");
    }
    
    public void OnGet() =>
        DisplayRoutes = _dbContext.Routes
            .AsNoTracking()
            .Include(r => r.Country)
            .Include(r => r.Purpose);

    public void OnPost() =>
        DisplayRoutes = _dbContext.Routes
            .AsNoTracking()
            .Where(p => p.PurposeId == PurposeId && p.Country!.TransferCost < TransferCost)
            .Include(r => r.Country)
            .Include(r => r.Purpose);
}