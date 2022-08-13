using Homework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Query7 : PageModel
{
    private TravelAgencyContext _dbContext;
    public IQueryable<Result>? QueryResult { get; set; }
    
    
    public Query7(TravelAgencyContext agencyContext) => _dbContext = agencyContext;
    
    public void OnGet() =>
        QueryResult = _dbContext.Travels
            .AsNoTracking()
            .GroupBy(tr => tr.Route!.Purpose!.Name,
                (key, group) => new Result(
                    key!,
                    group.Min(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost),
                    group.Average(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost),
                    group.Max(g => g.Route!.DailyCost + g.Route!.Country!.DailyCost)
                ));


    public record Result(string Purpose, int MinPrice, double AvgPrice, int MaxPrice);
}