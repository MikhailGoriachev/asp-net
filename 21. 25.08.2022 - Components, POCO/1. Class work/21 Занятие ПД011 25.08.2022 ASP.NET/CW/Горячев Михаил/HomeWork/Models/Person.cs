using Newtonsoft.Json;

namespace HomeWork.Models;

public class Person
{
    // имя
    public string? Name { get; set; }

    // рост
    public string? Height { get; set; }

    // масса
    public double? Mass { get; set; }

    // цвет волос
    public string? HairColor { get; set; }

    // цвет кожи
    public string? SkinColor { get; set; }

    // цвет глаз
    public string? YyeColor { get; set; }

    // год рождения
    public string? BirthYear { get; set; }

    // пол
    public string? Gender { get; set; }

    // родной мир
    public string? HomeWorld { get; set; }
    
    // ссылка на подробную информацию
    [JsonProperty("url")]
    public string? UrlInfo { get; set; }
}
