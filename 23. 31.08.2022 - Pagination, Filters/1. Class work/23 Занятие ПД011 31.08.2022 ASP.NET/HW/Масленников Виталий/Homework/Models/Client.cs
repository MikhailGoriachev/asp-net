using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

public class Client
{
    public int Id { get; set; }
    
    // Фамилия
    [Display(Name = "Фамилия")]
    public string? Surname { get; set; }
    // Имя
    [Display(Name = "Имя")]
    public string? Name { get; set; }
    // Отчество
    [Display(Name = "Отчество")]
    public string? Patronymic { get; set; }
    // Серия паспорта
    [Display(Name = "Паспорт")]
    public string? Passport { get; set; }


    public string FullName => $"{Surname} {Name?.First()}.{Patronymic?.First()}.";

    public List<Travel> Trips { get; set; } = new();
}