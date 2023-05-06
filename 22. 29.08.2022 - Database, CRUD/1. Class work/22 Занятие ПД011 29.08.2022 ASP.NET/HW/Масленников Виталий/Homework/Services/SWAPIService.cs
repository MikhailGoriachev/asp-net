using System.Diagnostics;
using System.Dynamic;
using System.Net;
using Homework.Common;
using Homework.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Homework.Services;

// Сервис работы с API https://swapi.dev
public class SWAPIService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _cache;

    public SWAPIService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }
    
    // Получить объект персонажа по id
    public async Task<Character?> GetCharacterInfoAsync(int id)
    {
        // Проверка на содержание в кэше по id 
        if (!_cache.TryGetValue(id, out Character character))
        {
            character = await GetDataAsync<Character>($"people/{id}");
            // Сохранить данные в кэш
            _cache.Set(id, character,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }
        return character;
    }

    // Получить объект планеты по url
    public async Task<Planet?> GetPlanetInfoAsync(string url)
    {
        var id = int.Parse(url[30..].TrimEnd('/'));
        return await GetPlanetInfoAsync(id);
    }

    // Получить объект планеты по id
    public async Task<Planet?> GetPlanetInfoAsync(int id)
    {
        // Проверка на содержание в кэше по id 
        if (!_cache.TryGetValue(id, out Planet planet))
        {
            planet = await GetDataAsync<Planet>($"planets/{id}");
            // Сохранить данные в кэш
            _cache.Set(id, planet,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));
        }
        
        return planet;
    }
    

    public async Task<List<Character>> GetCharactersListAsync(int amount)
    {
        var characters = await GetDataListAsync<Character>(amount, "people");
        
        // Проставить название планеты вмето url
        var tasks = characters!.Select(c =>
            GetPlanetInfoAsync(c.Homeworld).ContinueWith(t => c.Homeworld = t.Result!.Name));

        // Запуск всех задач
        await Task.WhenAll(tasks);
        
        return characters;
    }

    public async Task<List<Planet>> GetPlanetsListAsync(int amount) => 
        await GetDataListAsync<Planet>(amount, "planets");

    // Получение объекта данных по строке запроса
    private async Task<T> GetDataAsync<T>(string url)
    {
        var client = _httpClientFactory.CreateClient("SWAPI");
        var responseString = await client.GetStringAsync(url);
        
        return JsonConvert.DeserializeObject<T>(responseString, JsonSettings)!;
    }
    
    // Получение коллекции данных указанного количества
    private async Task<List<T>> GetDataListAsync<T>(int amount, string urlContextSegment) where T : IIdentificable
    {
        const int perPage = 10;
        // Количество страниц для загрузки
        int pages = (int)Math.Ceiling((double)amount / perPage);
        
        // Формирование задач для загрузки страниц
        var tasks = Enumerable.Range(1, pages)
            .Select(i => GetDataPageAsync<T>($"{urlContextSegment}/?page={i}"));
        
        // Параллельная отправка запросов и получение данных нужных страниц, склеивание в один список
        var dataList = (await Task.WhenAll(tasks))
            .SelectMany(x => x)
            .ToList();

        // Отсечение лишних данных
        dataList.RemoveRange(amount, dataList.Count - amount);
        
        // Проставить иднетификаторы
        int id = 1;
        dataList.ForEach(p => p.Id = id++);

        return dataList;
    }
    
    // Получение одной страницы данных
    private async Task<List<T>> GetDataPageAsync<T>(string url)
    {
        var client = _httpClientFactory.CreateClient("SWAPI");
        var responseString = await client.GetStringAsync(url);
        ApiResponse<T> response = JsonConvert.DeserializeObject<ApiResponse<T>>(responseString, JsonSettings)!;
        return response.Results!;
    }
    

    // Опции сопоставления CamelCase/snake_case при десереализации
    private static readonly JsonSerializerSettings JsonSettings = new()
        { ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() } };
}