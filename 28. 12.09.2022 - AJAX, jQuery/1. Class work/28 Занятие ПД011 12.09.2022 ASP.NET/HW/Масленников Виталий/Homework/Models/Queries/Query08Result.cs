namespace Homework.Models.Queries;

public class Query08Result
{

    // название товара
    public string? Goods { get; set; }

    public string? Units { get; set; }
    // средняя цена закупки
    public int? AvgPrice { get; set; }

    // количество закупок
    public int? Amount { get; set; }
}