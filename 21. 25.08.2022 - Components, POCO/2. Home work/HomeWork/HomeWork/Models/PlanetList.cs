using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Список планет
public class PlanetList
{
    // следующая страница с данными
    public string? Next { get; set; }

    // результат планеты
    [JsonProperty("results")]
    public List<Planet>? Planets { get; set; }
}
