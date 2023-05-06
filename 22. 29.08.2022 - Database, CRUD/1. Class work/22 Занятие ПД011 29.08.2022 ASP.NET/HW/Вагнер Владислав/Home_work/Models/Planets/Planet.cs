using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Home_work.Models.Planets;

public class Planet
{
    [System.Text.Json.Serialization.JsonIgnore]
    public int Id { get; set; } = 0;

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("rotation_period")]
    public string RotationPeriod { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public int? RotationPeriodInt { get; set; } = 0;

    [JsonProperty("orbital_period")]
    public string? OrbitalPeriod { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public int? OrbitalPeriodInt { get; set; } = 0;

    [JsonProperty("diameter")]
    public string? Diameter { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public int? DiameterInt { get; set; } = 0;

    [JsonProperty("climate")]
    public string Climate { get; set; }

    [JsonProperty("gravity")]
    public string Gravity { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public double GravityNumeric { get; set; } = 0d;

    [JsonProperty("terrain")]
    public string Terrain { get; set; }

    [JsonProperty("surface_water")]
    public string SurfaceWater { get; set; }

    [JsonProperty("population")]
    public string Population { get; set; }

    [JsonProperty("residents")]
    public List<string> Residents { get; set; }


    [JsonProperty("created")]
    public DateTime Created { get; set; }

    [JsonProperty("edited")]
    public DateTime Edited { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    public Planet(
            string name, string rotationPeriod, string orbitalPeriod, string diameter,
            string climate, string gravity, string terrain,
            string surfaceWater, string population, List<string> residents,
            DateTime created, DateTime edited, string url
        )
    {
        Name = name;
        RotationPeriod = rotationPeriod;
        OrbitalPeriod = orbitalPeriod;
        Diameter = diameter;
        Climate = climate;
        Gravity = gravity;
        Terrain = terrain;
        SurfaceWater = surfaceWater;
        Population = population;
        Residents = residents;
        Created = created;
        Edited = edited;
        Url = url;
    }
}
