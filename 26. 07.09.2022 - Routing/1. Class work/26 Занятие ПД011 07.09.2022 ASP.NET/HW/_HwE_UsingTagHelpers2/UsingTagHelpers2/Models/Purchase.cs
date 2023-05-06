namespace UsingTagHelpers2.Models;

/// <summary>
/// Товар	
/// *Количество проданных единиц товара
/// *Цена продажи единицы товара
/// *Дата продажи товара
/// </summary>
public class Purchase
{
    public int Id { get; set; }
    // Количество товара
    public int Amount { get; set; }
    // Цена закупки товара
    public int PricePurchase { get; set; }
    // Дата закупки товара
    public DateTime DatePurchase { get; set; }

    // Товар
    // связное свойство для стороны "один"
    public int GoodId { get; set; }
    public Good? Good { get; set; }


    // Единица измерения товара
    // связное свойство для стороны "один"
    public int UnitId { get; set; }
    public Unit? Unit { get; set; }

    public List<Sale> Sales { get; set; } = new();


}
