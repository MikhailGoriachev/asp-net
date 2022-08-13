using Homework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Groups : PageModel
{
    private PeriodicalsContext _dbContext;

    public List<GroupInfoMinMaxPrice>? GroupedMinMaxPrice { get; set; } 
    public List<GroupInfoAvgPrice>? GroupedAvgPrice { get; set; }
    
    public Groups(PeriodicalsContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task OnGetMinMaxPriceAsync()
    {
        GroupedMinMaxPrice = await _dbContext.Publications
            .AsNoTracking()
            .GroupBy(p => p.PubType, (key, group) => 
                new GroupInfoMinMaxPrice(key!
                    , group.Min(p => p.Price)
                    , group.Max(p => p.Price)
                    , group.Count()))
            .ToListAsync();
        
    }
    
    public async Task OnGetAvgPriceAsync()
    {
        GroupedAvgPrice = await _dbContext.Publications
            .AsNoTracking()
            .GroupBy(p => p.PubType, (key, group) => 
                new GroupInfoAvgPrice(key!
                    , group.Average(p => p.Price)
                    , group.Count()))
            .ToListAsync();
    }

    public record GroupInfoMinMaxPrice(string Type, int MinPrice, int MaxPrice, int Amount);
    public record GroupInfoAvgPrice(string Type, double AvgPrice, int Amount);
}