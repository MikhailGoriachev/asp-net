using System.Text.Json.Serialization;

namespace StarWarsUniverse.Models.Json
{
    public class PeopleJson
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("results")]
        public List<Person> People { get; set; }
    }
}
