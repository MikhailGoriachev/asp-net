namespace Homework.Models.Queries;

public class Query10Result
{
    // единицы измерения
    public Unit Unit { get; set; } = new();

    // сумма продаж
    public int? SoldSum { get; set; }
    
    // количества продано
    public int? SoldAmount { get; set; }
    
    
    // количество продаж
    public int? SalesAmount { get; set; }
    
    

    public Query10Result()
    {
    }
    
    public Query10Result(Unit unit, int? soldSum, int? soldAmount, int? salesAmount)
    {
        Unit = unit;
        SoldSum = soldSum;
        SoldAmount = soldAmount;
        SalesAmount = salesAmount;
    }
}
