using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models;

// Класс Продавец
public class Seller
{
    public int Id { get; set; }

    // фамилия
    [Required]
    public string Surname { get; set; } = "";

    // имя
    [Required]
    public string Name { get; set; } = "";

    // процент от продажи
    [Required]
    public string Patronymic { get; set; } = "";

    // процент от продажи
    [Required]
    [Range(1, int.MaxValue)]
    public int Interest { get; set; }


    // связное свойство Продажи
    public List<Sale>? Sales { get; set; }

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
