using System.ComponentModel.DataAnnotations;

namespace UsingTagHelpers2.Models.ViewModels;

// Класс Модель представления для фильтрации закупок
public class FilterByPurchaseViewModel
{
    // маршруты
    public IEnumerable<Purchase> Purchases { get; set; } = null!;

    // id цели поездки
    [Display(Name = "Наименование товара")]
    public int GoodId { get; set; }

    // id страна маршрута
    [Display(Name = "Ед. измерения")]
    public int UnitId { get; set; }


    // стоимость закупки
    [Display(Name = "Цена")]
    public int? Price { get; set; }

    // конструкторы
    public FilterByPurchaseViewModel()
    {
    }

    public FilterByPurchaseViewModel(IEnumerable<Purchase> purchases, int goodId, int unitId, int? price)
    {
        Purchases = purchases;
        GoodId = goodId;
        UnitId = unitId;
        Price = price;
    }


} // class FilterByPurchaseViewModel
