using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Список планет
public class PlanetList
{
    // количество записей
    public int Count { get; set; }

    // следующая страница с данными
    public string? Next { get; set; }

    // результат планеты
    [JsonProperty("results")]
    public List<Planet>? Planets { get; set; }
}
