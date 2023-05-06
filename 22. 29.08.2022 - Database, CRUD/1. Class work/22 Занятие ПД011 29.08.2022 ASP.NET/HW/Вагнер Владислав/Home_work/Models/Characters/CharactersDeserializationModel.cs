using System.Text.Json.Serialization;

namespace Home_work.Models.Characters;

public class CharactersDeserializationModel
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("results")]
    public List<Character> Characters { get; set; }

    public CharactersDeserializationModel(int count, List<Character> results, string next)
    {
        Count = count;
        Characters = results;
        Next = next;
    }

}
