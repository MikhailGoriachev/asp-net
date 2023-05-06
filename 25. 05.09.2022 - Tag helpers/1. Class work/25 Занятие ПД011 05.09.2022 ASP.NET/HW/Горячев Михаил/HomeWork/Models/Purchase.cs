using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models;

// Класс Закупка
public class Purchase
{
    public int Id { get; set; }

    // товар
    [Required] public int GoodsId { get; set; }
    public Goods? Goods { get; set; }

    // единица измерения
    [Required] public int UnitId { get; set; }
    public Unit? Unit { get; set; }

    // цена закупки
    [Required] [Range(0, int.MaxValue)] public int Price { get; set; }

    // количество единиц товара
    [Required] [Range(0, int.MaxValue)] public int Amount { get; set; }

    // дата закупки
    [Required] public DateTime DatePurchase { get; set; }


    // связное свойство Продажи
    public List<Sale>? Sales { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public Purchase()
    {

    }


    // конструктор инициализирующий
    public Purchase(int id, int goodsId, int unitId, int price, int amount, DateTime datePurchase)
    {
        Id = id;
        GoodsId = goodsId;
        UnitId = unitId;
        Price = price;
        Amount = amount;
        DatePurchase = datePurchase;
    }

    #endregion
}
