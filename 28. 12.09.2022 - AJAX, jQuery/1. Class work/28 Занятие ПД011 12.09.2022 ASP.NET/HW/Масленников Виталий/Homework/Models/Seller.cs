using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

// Модель данных продавца
public class Seller
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Фамилия")]
    public string Surname { get; set; } = "н/д";

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Имя")]
    public string Name { get; set; } = "н/д";

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Отчество")]
    public string Patronymic { get; set; } = "н/д";

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Паспорт")]
    public string Passport { get; set; } = "н/д";

    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Display(Name = "Комиссионные, %")]
    [Range(0, 100, ErrorMessage = "Недопустимое значение")]
    public double Interest { get; set; }

    public string ShortName => $"{Surname} {Name[0]}.{Patronymic[0]}.";

    public ICollection<Sale>? Sales { get; set; }
}