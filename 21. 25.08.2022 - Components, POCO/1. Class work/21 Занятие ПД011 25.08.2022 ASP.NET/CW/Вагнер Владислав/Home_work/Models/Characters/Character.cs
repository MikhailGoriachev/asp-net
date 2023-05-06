using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Home_work.Models.Characters;

public class Character
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; } = 0;

    //Имя
    [JsonPropertyName("name")]
    public string Name { get; set; }

    //Пол
    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    //Ссылка на подробную информацию
    [JsonPropertyName("url")]
    public string Url { get; set; }

    //Рост 
    [JsonPropertyName("height")]
    public string Height { get; set; }

    //Вес 
    [JsonPropertyName("mass")]
    public string Weight { get; set; }

    //Цвет волос
    [JsonPropertyName("hair_color")]
    public string HairColor { get; set; }

    //Цвет глаз
    [JsonPropertyName("eye_color")]
    public string EyeColor { get; set; }

    //Год рождения
    [JsonPropertyName("birth_year")]
    public string BirthYear { get; set; }

    public Character():this("", "", "", "", "", "", "", "")
    {
        
    }

    public Character(string name, string gender, string url, string height, string weight, string birth_year,string hairColor, string eyeColor)
    {
        Name = name;
        Gender = gender;
        Url = url;
        Height = height;
        Weight = weight;
        BirthYear = birth_year; 
        HairColor = hairColor;
        EyeColor = eyeColor;
    }
}
