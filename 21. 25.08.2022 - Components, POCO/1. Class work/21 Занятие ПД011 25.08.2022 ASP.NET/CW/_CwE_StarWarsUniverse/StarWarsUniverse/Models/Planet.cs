using System.Text.Json.Serialization;

namespace StarWarsUniverse.Models;

public class Planet
{
    public int Id { get; set; } = 0;

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("rotation_period")]
    public string RotationPeriod { get; set; }

    [JsonPropertyName("orbital_period")]
    public string OrbitalPeriod { get; set; }

    [JsonPropertyName("diameter")]
    public string Diameter { get; set; }

    [JsonPropertyName("climate")]
    public string Climate { get; set; }

    [JsonPropertyName("gravity")]
    public string Gravity { get; set; }

    [JsonPropertyName("terrain")]
    public string Terrain { get; set; }

    [JsonPropertyName("surface_water")]
    public string SurfaceWater { get; set; }

    [JsonPropertyName("population")]
    public string Population { get; set; }

    [JsonPropertyName("residents")]
    public List<string> Residents { get; set; }

    [JsonPropertyName("films")]
    public List<string> Films { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

