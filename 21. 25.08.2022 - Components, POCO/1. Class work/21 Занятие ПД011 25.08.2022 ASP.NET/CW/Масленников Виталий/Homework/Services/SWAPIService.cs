using System.Dynamic;
using Homework.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Homework.Services;

public class SWAPIService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SWAPIService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
    
    public async Task<List<Person>?> GetPersonsAsync()
    {
        var client = _httpClientFactory.CreateClient("SWAPI");
        var jsonString = await client.GetStringAsync("people");

        // Десерализация через анонимный тип и с опцией соотношения CamelCase/snake_case
        return JsonConvert.DeserializeAnonymousType(jsonString, new { results = new List<Person>() },
            JsonSettings)!.results;
    }
    
    public async Task<List<Planet>?> GetPlanetsAsync()
    {
        var client = _httpClientFactory.CreateClient("SWAPI");
        var jsonString = await client.GetStringAsync("planets");

        // Десерализация через анонимный тип и с опцией соотношения CamelCase/snake_case
        return JsonConvert.DeserializeAnonymousType(jsonString, new { results = new List<Planet>() },
            JsonSettings)!.results;
    }
    
    public async Task<Person?> GetPersonInfoAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("SWAPI");
        return await client.GetFromJsonAsync<Person>($"people/{id}");
    }
    
    public async Task<Person?> GetPlanetInfoAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("SWAPI");
        return await client.GetFromJsonAsync<Person>($"planet/{id}");
    }

    private static readonly JsonSerializerSettings JsonSettings = new()
        { ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() } };
}