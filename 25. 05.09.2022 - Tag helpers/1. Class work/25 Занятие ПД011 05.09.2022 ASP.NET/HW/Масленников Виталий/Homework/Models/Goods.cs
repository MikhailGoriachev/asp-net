using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

// Номенклатура товара
public class Goods
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Наименование")]
    public string Name { get; set; } = "н/д";

    public ICollection<Purchase>? Purchases { get; set; }
}