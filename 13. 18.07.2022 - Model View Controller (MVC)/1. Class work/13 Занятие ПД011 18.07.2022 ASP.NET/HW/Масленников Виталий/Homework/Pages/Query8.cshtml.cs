using Homework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Query8 : PageModel
{
    private TravelAgencyContext _dbContext;
    public IQueryable<Result>? QueryResult { get; set; }
    
    
    public Query8(TravelAgencyContext agencyContext) => _dbContext = agencyContext;
    
    public void OnGet() =>
        QueryResult = _dbContext.Routes
            .AsNoTracking()
            .GroupBy(route => route.Country!.Name,
                (key, group) => new Result(
                    key!,
                    group.Average(g => g.Country!.TransferCost)
                ));


    public record Result(string Country, double TransferAvg);
}