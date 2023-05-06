using System.ComponentModel.DataAnnotations;

namespace Homework.Models.TravelCompany;

public class Instructor
{
    // Идентификатор
    public int Id { get; set; }
    
    // Фамилия
    [Display(Name = "Фамилия:")]
    [Required(ErrorMessage = "Введите фамилию")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Surname { get; set; } = null!;
    
    // Имя 
    [Display(Name = "Имя:")]
    [Required(ErrorMessage = "Введите имя")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Name { get; set; } = null!;
    
    // Отчество
    [Display(Name = "Отчество:")]
    [Required(ErrorMessage = "Введите отчество")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Patronymic { get; set; } = null!;
    
    // Дата рождения
    [Display(Name = "Дата рождения:")]
    public DateTime DoB { get; set; }
    
    // Категория (A\B\C)
    [Display(Name = "Категория:")]
    [Required(ErrorMessage = "Выберите категорию")]
    public string Category { get; set; } = null!;
    
    public string ShortName => $"{Surname} {Name.First()}.{Patronymic.First()}.";
}