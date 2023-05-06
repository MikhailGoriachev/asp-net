using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Models;

// Класс Продажа
public class Sale
{
    [HiddenInput]
    public int Id { get; set; }

    // дата продажи
    [Display(Name = "Дата продажи")]
    [Required]
    public DateTime DateSell { get; set; } = DateTime.Now;

    // продавец
    [Display(Name = "Продавец")]
    [Required]
    public int SellerId { get; set; }

    public Seller? Seller { get; set; }

    // закупленный товар
    [Display(Name = "Закупленный товар")]
    [Required]
    public int PurchaseId { get; set; }

    public Purchase? Purchase { get; set; }

    // количество товара
    [Display(Name = "Количество товара")]
    [Required]
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }

    // цена продажи единицы товара
    [Display(Name = "Цена ед. товара")]
    [Required]
    [Range(0, int.MaxValue)]
    public int Price { get; set; }

    // единица измерения
    [Display(Name = "Единицы измерения")]
    [Required]
    public int UnitId { get; set; }
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
