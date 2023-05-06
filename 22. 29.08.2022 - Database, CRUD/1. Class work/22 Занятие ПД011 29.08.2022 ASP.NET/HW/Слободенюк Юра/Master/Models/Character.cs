using System.Globalization;
using System.Text.Json.Serialization;
using Master.Common;


namespace Master.Models;


public sealed class Character : IJsonOnDeserialized
{
    public int Id { get; private set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [JsonPropertyName("height")]
    public string? Height { get; init; }
    
    [JsonPropertyName("mass")]
    public string? Mass { get; init; }
    
    [JsonPropertyName("hair_color")]
    public string? HairColor { get; init; }
    
    [JsonPropertyName("skin_color")]
    public string? SkinColor { get; init; }
    
    [JsonPropertyName("eye_color")]
    public string? EyeColor { get; init; }
    
    [JsonPropertyName("birth_year")]
    public string? BirthYear { get; init; }
    
    [JsonPropertyName("gender")]
    public string? Gender { get; init; }
    
    [JsonPropertyName("homeworld")]
    public string? Homeworld { get; set; }
    
    [JsonPropertyName("films")]
    public IReadOnlyCollection<string>? Films { get; init; }
    
    [JsonPropertyName("species")]
    public IReadOnlyCollection<string>? Species { get; init; }
    
    [JsonPropertyName("starships")]
    public IReadOnlyCollection<string>? Starships  { get; init; }
    
    [JsonPropertyName("vehicles")]
    public IReadOnlyCollection<string>? Vehicles  { get; init; }
    
    [JsonPropertyName("url")]
    public Uri? DetailsUrl  { get; init; }
    
    [JsonPropertyName("created")]
    public DateTime Created  { get; init; }
    
    [JsonPropertyName("edited")]
    public DateTime Edited  { get; init; }


    public void OnDeserialized()
    {
        if (DetailsUrl is null)
            throw new("Character without id");

        var idSpan = DetailsUrl.ExtractSegment(^1);

        if (int.TryParse(idSpan, out var id) == false)
            throw new("Character without id");
        
        Id = id;
    }
}