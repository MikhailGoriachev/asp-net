using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Models.ViewModels;

public class FilterViewModel
{
    public SelectList? UnitsList { get; set; }
    
    public SelectList? GoodsList { get; set; }

    public string? UnitsSelected { get; set; }
    
    public string? GoodsSelected { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Неверное значение минимальной цены закупки")]
    public int? PriceFrom { get; set; } 
    
    [Range(0, int.MaxValue, ErrorMessage = "Неверное значение минимальной цены закупки")]
    public int? PriceTo { get; set; }

    public string? Sort { get; set; }
    
    public List<RouteParameter> GetRouteParameters => new ()
    {
        new RouteParameter { Key = nameof(UnitsSelected), Value = UnitsSelected },
        new RouteParameter { Key = nameof(GoodsSelected), Value = GoodsSelected },
        new RouteParameter { Key = nameof(PriceFrom), Value = $"{PriceFrom}" },
        new RouteParameter { Key = nameof(PriceTo), Value = $"{PriceTo}" },
    };
}