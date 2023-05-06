using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Планета
public class Planet
{
    // название
    public string? Name { get; set; }

    // период вращения
    public string? RotationPeriod { get; set; }

    // орбитальный период
    public string? OrbitalPeriod { get; set; }

    // диаметр
    public double Diameter { get; set; }

    // климат
    public string? Climate { get; set; }

    // гравитация
    public string? Gravity { get; set; }

    // местность
    public string? Terrain { get; set; }

    // водная поверхность
    public string? SurfaceWater { get; set; }

    // население
    public string? Population { get; set; }

    // ссылка на подробную информацию
    [JsonProperty("url")]
    public string? UrlInfo { get; set; }
}
