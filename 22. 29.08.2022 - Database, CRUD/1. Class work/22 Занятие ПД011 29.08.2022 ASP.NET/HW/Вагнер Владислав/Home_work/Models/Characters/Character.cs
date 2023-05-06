using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Home_work.Models.Characters;

public class Character
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; } = 0;

    //Имя
    public string Name { get; set; }

    //Рост
    public string Height { get; set; }

    //Вес
    public string? Mass { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    //Вес в double
    public double? WeightDouble { get; set; }

    //Цвет волос
    [JsonProperty("hair_color")]
    public string HairColor { get; set; } = "";

    //Цвет кожи
    [JsonProperty("skin_color")]
    public string SkinColor { get; set; } = "";

    //Цвет глаз
    [JsonProperty("eye_color")]
    public string EyeColor { get; set; } = "";

    //Год рождения
    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; }

    //Пол
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    //Планета
    [JsonPropertyName("homeworld")]
    public string Homeworld { get; set; }


    #region Списки

    [JsonPropertyName("vehicles")]
    public List<string> Vehicles { get; set; }

    [JsonPropertyName("starships")]
    public List<string> Starships { get; set; }

    [JsonPropertyName("films")]
    public List<string> Films { get; set; }

    #endregion


    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    public Character(string name, string gender,
        string url, string height, string weight, string birth_year,
        string hairColor, string eyeColor, List<string> films,List<string> vehicles, List<string> starships,
        DateTime created, DateTime edited, double? weightDouble)
    {
        Name = name;
        Gender = gender;
        Url = url;
        Height = height;
        Mass = weight;
        BirthYear = birth_year;
        HairColor = hairColor;
        EyeColor = eyeColor;
        Films = films;
        Vehicles = vehicles;
        Starships = starships;
        Created = created;
        Edited = edited;
        WeightDouble = weightDouble;
    }

}
