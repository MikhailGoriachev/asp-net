using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Планета
public class Planet
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

    // название
    [Display(Name = "Название")]
    public string? Name { get; set; }

    // период вращения
    [Display(Name = "Период вращения")]
    public string? RotationPeriod { get; set; }

    // орбитальный период
    [Display(Name = "Орбитальный период")]
    public double OrbitalPeriod { get; set; }

    // диаметр
    [Display(Name = "Диаметр")]
    public double Diameter { get; set; }

    // климат
    [Display(Name = "Климат")]
    public string? Climate { get; set; }

    // гравитация
    [Display(Name = "Гравитация")]
    public string? Gravity { get; set; }

    // поверхность
    [Display(Name = "Поверхность")]
    public string? Terrain { get; set; }

    // водная поверхность
    [Display(Name = "Водная поверхность")]
    public string? SurfaceWater { get; set; }

    // население
    [Display(Name = "Население")]
    public string? Population { get; set; }

    // ссылка на подробную информацию
    [JsonProperty("url")] public string? UrlInfo { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Planet() => Id = ++_currentMaxId;

    #endregion
}
