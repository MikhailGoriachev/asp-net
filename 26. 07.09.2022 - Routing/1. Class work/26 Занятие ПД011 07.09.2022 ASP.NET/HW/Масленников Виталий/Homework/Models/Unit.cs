using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

// Единица измерения товара
public class Unit
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Единицы (кратко)")]
    public string Short { get; set; } = "н/д";
    
    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Единицы (полностью)")]
    public string Long { get; set; } = "н/д";
    
    public ICollection<Sale>? Sales { get; set; }
    public ICollection<Purchase>? Purchases { get; set; }
}