namespace HomeWork.Models.Queries;

// Результат запроса 7
// *Дата продажи,
// *Наименование товара,
// *Цена закупки,
// *Цена продажи,
// *Количество проданных единиц,
// *Прибыль
public record Query07Result(DateTime SellDate, string ProductName, int PurchasePrice,  
                            int SellPrice, int AmountSale, int Profit);

