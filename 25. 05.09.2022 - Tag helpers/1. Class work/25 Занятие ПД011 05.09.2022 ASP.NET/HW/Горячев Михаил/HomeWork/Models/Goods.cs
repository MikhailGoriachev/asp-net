using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HomeWork.Models;

// Класс Товар
public class Goods
{
    public int Id { get; set; }

    // название товара
    [Required] public string Name { get; set; } = "";


    // связное свойство Закупки
    public List<Purchase>? Purchases { get; set; }



    #region Конструкторы

    // конструктор по умолчанию
    public Goods()
    {

    }

    // конструктор инициализирующий
    public Goods(int id, string name)
    {
        Id = id;
        Name = name;
    }

    #endregion
}
