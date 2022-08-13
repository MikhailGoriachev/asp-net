using Application.Common;
using Application.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Application.Pages;


public class Second : PageModel
{
    public IEnumerable<Product> Products { get; set; } = new[]
    {
        new Product("Name1", "Code1", 1, 1, "1.png"),
        new Product("Name2", "Code2", 2, 2, "2.png"),
        new Product("Name1", "Code1", 1, 1, "1.png"),
        new Product("Name6", "Code6", 6, 6, "6.png"),
        new Product("Name5", "Code5", 5, 5, "5.png"),
        new Product("Name4", "Code4", 4, 4, "4.png"),
        new Product("Name2", "Code2", 2, 2, "2.png"),
        new Product("Name6", "Code6", 6, 6, "6.png"),
        new Product("Name1", "Code1", 1, 1, "1.png"),
        new Product("Name1", "Code1", 1, 1, "1.png"),
        new Product("Name3", "Code3", 3, 3, "3.png"),
        new Product("Name3", "Code3", 3, 3, "3.png"),
        new Product("Name2", "Code2", 2, 2, "2.png"),
        new Product("Name5", "Code5", 5, 5, "5.png"),
        new Product("Name6", "Code6", 6, 6, "6.png"),
    };


    public void OnGetOrder(string prop, SortOrder order)
    {
        Products = Sorterer<Product>.OrderBy(Products, prop, order);
    }
}