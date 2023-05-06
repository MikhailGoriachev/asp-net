namespace WebApiDatabase.Models.Queries;

// тип данных для одной запсиси выборки по запросу 9
public record Query09Result(
    string SellerFullName, 
    double AvgSellPrice, 
    int Count
 );

