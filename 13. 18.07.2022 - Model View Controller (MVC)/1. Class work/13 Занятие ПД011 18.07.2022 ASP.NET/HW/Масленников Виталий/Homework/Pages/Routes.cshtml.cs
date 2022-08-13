using Homework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Route = Homework.Models.Route;

namespace Homework.Pages;

public class Routes : PageModel
{
    private TravelAgencyContext _dbContext;

    // Коллекция данных для отображения
    public IQueryable<Route>? DisplayRoutes { get; private set; }

    public Routes(TravelAgencyContext agencyContext) => _dbContext = agencyContext;

    public void OnGet() => DisplayRoutes = _dbContext.Routes
        .Include(r => r.Country)
        .Include(r => r.Purpose)
        .AsNoTracking();
}