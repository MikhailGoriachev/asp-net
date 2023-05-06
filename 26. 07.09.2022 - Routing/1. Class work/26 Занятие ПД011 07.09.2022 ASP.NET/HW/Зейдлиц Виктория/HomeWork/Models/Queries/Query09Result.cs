namespace HomeWork.Models.Queries;

// Результат запроса 9
// *ФИО продавца
// *Средняя цена продажи
// *Количество продажи
public record Query09Result(string FullName, double AvgSale, int AmountSale);

