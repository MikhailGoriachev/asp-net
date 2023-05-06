using System.Text.Json.Serialization;

namespace HomeWork.Models.Task1;

public class PeopleJson
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonPropertyName("previous")]
    public object? Previous { get; set; }

    [JsonPropertyName("results")]
    public List<Person>? Results { get; set; }

} // class PeopleJson
