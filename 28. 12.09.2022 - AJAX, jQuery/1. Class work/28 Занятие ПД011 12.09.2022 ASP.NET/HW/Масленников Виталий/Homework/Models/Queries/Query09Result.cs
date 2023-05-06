namespace Homework.Models.Queries;

public class Query09Result
{
    // продавец
    public string? Seller { get; set; } 

    // средняя цена продажи единицы товара
    public int? AvgPrice { get; set; }
    
    // количество продаж
    public int? AmountSales { get; set; }
}
