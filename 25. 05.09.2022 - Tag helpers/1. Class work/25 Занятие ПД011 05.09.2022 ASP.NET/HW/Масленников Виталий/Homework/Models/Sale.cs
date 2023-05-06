using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

// Факт продажи товара
public class Sale
{
    [Required]
    public int Id { get; set; }
    
    public int PurchaseId { get; set; }
    
    public int UnitId { get; set; }
    
    public int SellerId { get; set; }
    
    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Дата продажи")]
    public DateTime SaleDate { get; set; }
    
    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Цена продажи")]
    [Range(0, int.MaxValue, ErrorMessage = "Недопустимое значение")]
    public int Price { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Количество")]
    [Range(0, int.MaxValue, ErrorMessage = "Недопустимое значение")]
    public int Amount { get; set; }

    public Purchase? Purchase { get; set; }
    public Unit? Unit { get; set; }
    public Seller? Seller { get; set; }
}