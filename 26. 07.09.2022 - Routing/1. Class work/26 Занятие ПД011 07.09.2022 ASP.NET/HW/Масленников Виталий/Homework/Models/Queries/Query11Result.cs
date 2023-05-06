namespace Homework.Models.Queries;

public class Query11Result
{
    // продавец
    public Seller Seller { get; set; } = new();

    // минимальная стоимость продаж
    public int? MinSale { get; set; }

    // максимальная стоимость продаж
    public int? MaxSale { get; set; }

    // количество продаж
    public int? AmountSales { get; set; }
    
    // сумма продаж
    public int? SumSales { get; set; }
}
