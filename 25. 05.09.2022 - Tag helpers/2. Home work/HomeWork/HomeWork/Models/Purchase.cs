using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Models;

// Класс Закупка
public class Purchase
{
    [HiddenInput] public int Id { get; set; }

    // товар

    [Display(Name = "Товар")] [Required] public int GoodsId { get; set; }
    public Goods? Goods { get; set; }

    // единица измерения
    [Display(Name = "Единица измерения")]
    [Required]
    public int UnitId { get; set; }

    public Unit? Unit { get; set; }

    // цена закупки
    [Display(Name = "Цена закупки")]
    [Required]
    [Range(0, int.MaxValue)]
    public int Price { get; set; }

    // количество единиц товара
    [Display(Name = "Количество")]
    [Required]
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }

    // дата закупки
    [Required]
    [Display(Name = "Дата закупки")]
    public DateTime DatePurchase { get; set; } = DateTime.Now;


    // связное свойство Продажи
    public List<Sale>? Sales { get; set; }


    // строковая информация о покупке
    public string StringInfoPurchase => $"Id: {Id}; Товар: {Goods?.Name}; Цена: {Price}; Дата закупки: {DatePurchase}";

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
