using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Models;

// Класс Продавец
public class Seller
{
    [HiddenInput]
    public int Id { get; set; }

    // фамилия
    [Display(Name = "Фамилия")]
    [Required]
    public string Surname { get; set; } = "";

    // имя
    [Display(Name = "Имя")]
    [Required]
    public string Name { get; set; } = "";

    // отчество
    [Display(Name = "Отчество")]
    [Required]
    public string Patronymic { get; set; } = "";

    // процент от продажи
    [Display(Name = "Процент от продажи")]
    [Required]
    [Range(1, int.MaxValue)]
    public int Interest { get; set; }


    // связное свойство Продажи
    public List<Sale>? Sales { get; set; }


    // id и полное имя
    public string IdAndFullName => $"{Id}. {Surname} {Name} {Patronymic}";

    #region Конструкторы

    // конструктор по умолчанию
    public Seller()
    {

    }


    // конструктор инициализирующий
    public Seller(int id, string surname, string name, string patronymic, int interest)
    {
        Id = id;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Interest = interest;
    }

    #endregion
}
