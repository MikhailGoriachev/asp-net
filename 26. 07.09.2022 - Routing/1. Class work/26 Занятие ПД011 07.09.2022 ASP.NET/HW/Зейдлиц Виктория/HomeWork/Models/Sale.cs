namespace HomeWork.Models;

/// <summary>
/// Продажа товара	
/// *Количество проданных единиц товара
/// *Цена продажи единицы товара
/// *Дата продажи товара
/// </summary>
public class Sale
{
    public int Id { get; set; }
    // Количество проданных единиц товара
    public int AmountSale { get; set; }
    // Цена продажи единицы товара
    public int PriceSale { get; set; }
    // Дата продажи товара
    public DateTime DateSale { get; set; }

    // Закупка товара
    // связное свойство для стороны "один"
    public int PurchaseId { get; set; }
    public Purchase? Purchase { get; set; }


    // Продавец
    // связное свойство для стороны "один"
    public int SellerId { get; set; }
    public Seller? Seller { get; set; }
}

