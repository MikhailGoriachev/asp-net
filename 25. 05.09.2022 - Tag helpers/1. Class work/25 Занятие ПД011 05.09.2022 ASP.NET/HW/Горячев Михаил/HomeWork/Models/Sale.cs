using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models;

// Класс Продажа
public class Sale
{
    public int Id { get; set; }

    // дата продажи
    [Required] public DateTime DateSell { get; set; }

    // продавец
    [Required] public int SellerId { get; set; }
    public Seller? Seller { get; set; }

    // закупленный товар
    [Required] public int PurchaseId { get; set; }
    public Purchase? Purchase { get; set; }

    // количество товара
    [Required] [Range(0, int.MaxValue)] public int Amount { get; set; }

    // цена продажи единицы товара
    [Required] [Range(0, int.MaxValue)] public int Price { get; set; }

    // единица измерения
    [Required] public int UnitId { get; set; }
    public Unit? Unit { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Sale()
    {

    }


    // конструктор инициализируюший
    public Sale(int id, DateTime dateSell, int sellerId, int purchaseId, int amount, int price, int unitId)
    {
        Id = id;
        DateSell = dateSell;
        SellerId = sellerId;
        PurchaseId = purchaseId;
        Amount = amount;
        Price = price;
        UnitId = unitId;
    }

    #endregion
}
