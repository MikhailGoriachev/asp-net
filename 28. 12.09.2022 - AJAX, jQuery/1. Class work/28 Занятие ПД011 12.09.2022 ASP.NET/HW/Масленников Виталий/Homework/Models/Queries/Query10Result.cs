namespace Homework.Models.Queries;

public class Query10Result
{
    // единицы измерения
    public string Unit { get; set; } 

    // сумма продаж
    public int? SoldSum { get; set; }
    
    // количества продано
    public int? SoldAmount { get; set; }
    
    // количество продаж
    public int? SalesAmount { get; set; }
    
    
    public Query10Result()
    {
    }
    
    public Query10Result(string unit, int? soldSum, int? soldAmount, int? salesAmount)
    {
        Unit = unit;
        SoldSum = soldSum;
        SoldAmount = soldAmount;
        SalesAmount = salesAmount;
    }
}
