using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HomeWork.Models.Task1;

public class Person
{
    [UIHint("HiddenInput")]
    [JsonIgnore]
    public int Id { get; set; } = 0;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("height")]
    public string? Height { get; set; }

    [JsonPropertyName("mass")]
    public string? Mass { get; set; }

    [JsonPropertyName("hair_color")]
    public string? Hair_Color { get; set; }

    [JsonPropertyName("skin_color")]
    public string? Skin_Color { get; set; }

    [JsonPropertyName("eye_color")]
    public string? Eye_Color { get; set; }

    [JsonPropertyName("birth_year")]
    public string? Birth_Year { get; set; }

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("homeworld")]
    public string? Homeworld { get; set; }

    [JsonPropertyName("films")]
    public List<string>? Films { get; set; }

    [JsonPropertyName("species")]
    public List<string>? Species { get; set; }

    [JsonPropertyName("vehicles")]
    public List<string>? Vehicles { get; set; }

    [JsonPropertyName("starships")]
    public List<string>? Starships { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

} // class Person
