using System.Text.Json.Serialization;


namespace Master.Models;


public sealed class Paginated<T>
{
    [JsonPropertyName("count")]
    public int Total { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyCollection<T>? Results { get; init; }
}