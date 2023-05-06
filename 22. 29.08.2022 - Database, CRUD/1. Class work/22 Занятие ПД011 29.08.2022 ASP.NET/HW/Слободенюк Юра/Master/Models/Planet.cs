using System.Text.Json.Serialization;
using Master.Common;


namespace Master.Models;


public sealed class Planet : IJsonOnDeserialized
{
    public int Id { get; private set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("rotation_period")]
    public string? RotationPeriod { get; init; }

    [JsonPropertyName("orbital_period")]
    public string? OrbitalPeriod { get; init; }

    [JsonPropertyName("diameter")]
    public string? Diameter { get; init; }

    [JsonPropertyName("climate")]
    public string? Climate { get; init; }

    [JsonPropertyName("gravity")]
    public string? Gravity { get; init; }

    [JsonPropertyName("terrain")]
    public string? Terrain { get; init; }

    [JsonPropertyName("surface_water")]
    public string? SurfaceWater { get; init; }

    [JsonPropertyName("population")]
    public string? Population { get; init; }

    [JsonPropertyName("residents")]
    public IReadOnlyCollection<string>? Residents { get; init; }

    [JsonPropertyName("films")]
    public IReadOnlyCollection<string>? Films { get; init; } 

    [JsonPropertyName("created")]
    public DateTime Created { get; init; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; init; }

    [JsonPropertyName("url")]
    public Uri? DetailsUrl { get; init; }


    public void OnDeserialized()
    {
        if (DetailsUrl is null)
            throw new("Planet without id");

        var idSpan = DetailsUrl.ExtractSegment(^1);

        if (int.TryParse(idSpan, out var id) == false)
            throw new("Planet without id");
        
        Id = id;
    }


    public string[] GetTerrains() => Terrain!
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToArray();
}