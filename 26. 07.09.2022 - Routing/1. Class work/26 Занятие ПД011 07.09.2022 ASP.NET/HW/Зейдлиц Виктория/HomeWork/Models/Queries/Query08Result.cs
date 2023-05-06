namespace HomeWork.Models.Queries;

// Результат запроса 8
// *Название товара
// *Количество закупки товара
// *Средняя цена закупки
public record Query08Result(string GoodsName,  int AmoutPurchase, double AvgPurchase);

