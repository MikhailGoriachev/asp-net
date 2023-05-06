using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HomeWork.Models;

public class Person
{
    // текущий максимальный id
    private static int _currentMaxId = 0;

    public static int CurrentMaxId
    {
        get => _currentMaxId;
        set => _currentMaxId = value;
    }

    // id
    private int _id;

    [Display(Name = "ID")]
    public int Id
    {
        get => _id;
        set
        {
            _currentMaxId = value > _currentMaxId ? value : _currentMaxId;
            _id = value;
        }
    }

    // имя
    [Display(Name = "Имя")]
    public string? Name { get; set; }

    // рост
    [Display(Name = "Рост")]
    public string? Height { get; set; }

    // масса
    [Display(Name = "Масса")]
    public double? Mass { get; set; }

    // цвет волос
    [Display(Name = "Цвет волос")]
    public string? HairColor { get; set; }

    // цвет кожи
    [Display(Name = "Цвет кожи")]
    public string? SkinColor { get; set; }

    // цвет глаз
    [Display(Name = "Цвет глаз")]
    public string? EyeColor { get; set; }

    // год рождения
    [Display(Name = "Год рождения")]
    public string? BirthYear { get; set; }

    // пол
    [Display(Name = "Пол")]
    public string? Gender { get; set; }

    // родной мир
    [Display(Name = "Родной мир")]
    [JsonProperty("homeworld")]
    public string? HomeWorld { get; set; }

    // ссылка на подробную информацию
    [JsonProperty("url")] public string? UrlInfo { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Person() => Id = ++_currentMaxId;

    #endregion
}
