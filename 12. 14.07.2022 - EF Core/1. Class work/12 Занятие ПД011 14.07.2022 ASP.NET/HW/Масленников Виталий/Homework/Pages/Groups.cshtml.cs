using Homework.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Groups : PageModel
{
    private PeriodicalsContext _dbContext;

    public IQueryable<GroupInfoMinMaxPrice>? GroupedMinMaxPrice { get; set; } 
    public IQueryable<GroupInfoAvgPrice>? GroupedAvgPrice { get; set; }
    public IQueryable<GroupInfoDurAvgPrice>? GroupedDurAvgPrice { get; set; }
    
    public Groups(PeriodicalsContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    //  группировка по полю Вид издания. Для каждого вида вычисляет среднюю цену 1 экземпляра,
    // количество изданий в группе
    public void OnGetMinMaxPriceAsync() =>
        GroupedMinMaxPrice = _dbContext.Publications
            .GroupBy(p => p.Category!.CategoryName, (key, group) => 
                new GroupInfoMinMaxPrice(
                    key!, group.Min(p => p.Price), group.Max(p => p.Price), group.Count()));

    // группировка по полю Вид издания. Для каждого вида вычисляет максимальную и минимальную цену 1 экземпляра,
    // количество изданий в группе
    public void OnGetAvgPriceAsync() =>
        GroupedAvgPrice = _dbContext.Publications.GroupBy(p => 
            p.Category!.CategoryName, (key, group) => 
            new GroupInfoAvgPrice(key!, group.Average(p => p.Price), group.Count()));
    
    // группировка по полю Длительность подписки. Для каждого срока вычисляет среднюю цену 1 экземпляра
    public void OnGetDurationsAvgPriceAsync() =>
        GroupedDurAvgPrice = _dbContext.Publications.AsNoTracking()
            .GroupBy(p => p.Duration, (key, group) => 
                new GroupInfoDurAvgPrice(key, group.Average(p => p.Price)));

    public record GroupInfoMinMaxPrice(string Type, int MinPrice, int MaxPrice, int Amount);
    public record GroupInfoAvgPrice(string Type, double AvgPrice, int Amount);
    public record GroupInfoDurAvgPrice(int Duration, double AvgPrice);
}