using Homework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Query6 : PageModel
{
    private TravelAgencyContext _dbContext;

    // Коллекция данных для отображения
    public IEnumerable<Result>? QueryResult { get; private set; }

    public Query6(TravelAgencyContext agencyContext) => _dbContext = agencyContext;

    public void OnGet() =>
        QueryResult = _dbContext.Travels
            .AsNoTracking()
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Purpose)
            .Include(tr => tr.Route)
            .ThenInclude(r => r!.Country)
            .ToList()
            .Select(travel => new Result(
                travel.Route!.Country!.Name!,
                travel.Route!.Purpose!.Name!,
                travel.StartDate,
                travel.Duration,
                (int)Math.Floor((travel.Duration * (travel.Route!.DailyCost + travel.Route!.Country!.DailyCost)
                                 + travel.Route.Country.TransferCost + travel.Route.Country.VisaCost) * 1.03)
            )).OrderBy(tr => tr.Country);

    public record Result(string Country, string Purpose, DateTime StartDate, int Duration, int TotalCost);
}