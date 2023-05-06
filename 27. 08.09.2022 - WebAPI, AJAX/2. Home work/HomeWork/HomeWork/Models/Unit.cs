using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Models;

// Класс Единицы измерения
public class Unit
{
    [HiddenInput]
    public int Id { get; set; }

    // сокращённое название
    [Display(Name = "Сокращённое название")]
    [Required]
    public string ShortName { get; set; } = "";

    // полное название
    [Display(Name = "Полное название")]
    [Required]
    public string LongName { get; set; } = "";


    // связное свойство Закупки
    public List<Purchase>? Purchases { get; set; }

    // связное свойство Продажи
    public List<Sale>? Sales { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public Unit()
    {
    }


    // конструктор инициализирующий
    public Unit(int id, string longName, string shortName)
    {
        Id = id;
        ShortName = shortName;
        LongName = longName;
    }

    #endregion
}
