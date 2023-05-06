using System.Text.Json.Serialization;

namespace Home_work.Models.Planets;

public class PlanetsDeserializationModel
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("results")]
    public List<Planet> PlanetsList { get; set; }

    public PlanetsDeserializationModel(int count, List<Planet> results, string next)
    {
        Count = count;
        PlanetsList = results;
        Next = next;
    }

}
