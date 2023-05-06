namespace Homework.Models.Queries;

// Результат запроса 6
// Дата продажи, Наименование товара, Цена закупки, Цена продажи, Количество проданных единиц, Прибыль
public record Query07Result(
    int Id, 
    DateTime SellDate,  // Дата продажи
    string Goods,       // Наименование товара
    int PurchasePrice,  // Цена закупки
    int SellPrice,      // Цена продажи
    int Amount,         // Количество проданных единиц
    int Profit          // Прибыль
);

