using System.ComponentModel.DataAnnotations;

namespace Homework.Models.TravelCompany;

public class Instructor
{
    // Идентификатор
    public int Id { get; set; }
    
    [Display(Name = "Фамилия:")]
    // Фамилия
    public string Surname { get; set; } = null!;
    
    // Имя 
    [Display(Name = "Имя:")]
    public string Name { get; set; } = null!;
    
    // Отчество
    [Display(Name = "Отчество:")]
    public string Patronymic { get; set; } = null!;
    
    // Дата рождения
    [Display(Name = "Дата рождения:")]
    public DateTime DoB { get; set; }
    
    // Категория (A\B\C)
    [Display(Name = "Категория:")]
    public string Category { get; set; } = null!;


    public string ShortName => $"{Surname} {Name.First()}.{Patronymic.First()}.";
}