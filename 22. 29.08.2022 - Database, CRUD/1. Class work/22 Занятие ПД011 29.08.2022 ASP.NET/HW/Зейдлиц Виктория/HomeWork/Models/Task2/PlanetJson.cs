using System.Text.Json.Serialization;

namespace HomeWork.Models.Task2;

public class PlanetJson
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonPropertyName("previous")]
    public object? Previous { get; set; }

    [JsonPropertyName("results")]
    public List<Planet>? Results { get; set; }

} // class PlanetJson
