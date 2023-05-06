namespace WebApiDatabase.Models.Queries;

// тип данных для одной запсиси выборки по запросу 8
public record Query08Result(
    string ProductName, 
    double AvgPurhaseprice, 
    int Count
);
