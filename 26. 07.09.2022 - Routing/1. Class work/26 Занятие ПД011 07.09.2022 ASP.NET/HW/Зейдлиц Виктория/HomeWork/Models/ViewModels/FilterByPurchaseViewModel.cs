using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models.ViewModels;

// Класс Модель представления для фильтрации закупок
public class FilterByPurchaseViewModel
{
    // закупки
    public IEnumerable<Purchase> Purchases { get; set; } = null!;

    // id цели поездки
    [Display(Name = "Наименование товара")]
    public int GoodId { get; set; }

    // id страна маршрута
    [Display(Name = "Ед. измерения")]
    public int UnitId { get; set; }

    // минимальная цена закупки
    [Display(Name = "Мин. цена")]
    public int? MinPrice { get; set; }

    // максимальная цена закупки
    [Display(Name = "Макс. цена")]
    public int? MaxPrice { get; set; }

    // конструкторы
    public FilterByPurchaseViewModel()
    {
    }

    public FilterByPurchaseViewModel(IEnumerable<Purchase> purchases, int goodId, int unitId, int? maxprice, int? minprice)
    {
        Purchases = purchases;
        GoodId = goodId;
        UnitId = unitId;
        MaxPrice = maxprice;
        MinPrice = minprice;
    }


} // class FilterByPurchaseViewModel
