using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Models;

// Класс Клиент
public class Client
{
    // идентификатор
    [HiddenInput]
    public int Id { get; set; }

    // фамилия
    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Это поле не может быть пустым!")]
    public string? Surname { get; set; }

    // имя
    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Это поле не может быть пустым!")]
    public string? Name { get; set; }

    // отчество
    [Display(Name = "Отчество")]
    [Required(ErrorMessage = "Это поле не может быть пустым!")]
    public string? Patronymic { get; set; }

    // паспорт
    [Display(Name = "Паспорт")]
    [Required(ErrorMessage = "Это поле не может быть пустым!")]
    public string? Passport { get; set; }


    // поездки (отношение к "многим")
    public List<Visit>? Visits { get; set; }

    // строковое представление клиента
    public string FullDataClient => $"{Surname!} {Name} {Patronymic} {Passport}";

    #region Констуркторы

    // конструктор по умолчанию
    public Client()
    {
    }


    // конструктор инициализирущий
    public Client(int id, string? surname, string? name, string? patronymic, string? passport)
    {
        Id = id;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Passport = passport;
    }

    #endregion
}
