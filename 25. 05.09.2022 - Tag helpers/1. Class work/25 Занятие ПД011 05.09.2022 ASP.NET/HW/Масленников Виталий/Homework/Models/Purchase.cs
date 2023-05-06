using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

// Факт закупки товара
public class Purchase
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Наименование")]
    public int GoodsId { get; set; }
    
    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Единицы")]
    public int UnitId { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Дата закупки")]
    public DateTime PurchaseDate { get; set; }
    
    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Цена закупки")]
    [Range(0, int.MaxValue, ErrorMessage = "Недопустимое значение")]
    public int Price { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Количество")]
    [Range(0, int.MaxValue, ErrorMessage = "Недопустимое значение")]
    public int Amount { get; set; }
    
    
    public Goods? Goods { get; set; }
    public Unit? Unit { get; set; }
    
    public ICollection<Sale>? Sales { get; set; }
}