using System.Diagnostics;
using System.Numerics;
using HomeWork.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HomeWork.Services;

// Сервис для получения данных
public class StarWarsService
{
    // параметры десериализации
    private static JsonSerializerSettings _jsonSerializerSettings;


    #region Конструкторы

    // конструктор статический
    static StarWarsService()
    {
        _jsonSerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },

            // подавление исключений при сериализации
            Error = (s, err) => err.ErrorContext.Handled = true
        };
    }

    #endregion

    #region Методы

    // получение данных по URL
    public async Task<T?> GetDataAsync<T>(string url) =>
        JsonConvert.DeserializeObject<T>(
            await (await new HttpClient().GetAsync(url)).Content.ReadAsStringAsync(), _jsonSerializerSettings
        );


    // получить словарь названий планет и url с подробной информацией
    public Task<Dictionary<string, Planet>?> GetPlanetDictionaryAsync() =>
        Task.Run(async () => (await GetPlanetsAsync(null))?.Planets?.ToDictionary(p => p.UrlInfo!));


    // получить список названий планет
    public Task<List<string?>?> GetPlanetNameListAsync() =>
        Task.Run(async () => (await GetPlanetsAsync(null))?.Planets?.Select(p => p.Name).Distinct().ToList());


    // получить список поверхностей планет
    public Task<IEnumerable<string>> GetPlanetTerrainListAsync() =>
        Task.Run(async () =>
        {
            var list = (await GetPlanetsAsync(null))?.Planets?.Select(p =>
                p.Terrain!.Split(", ", StringSplitOptions.RemoveEmptyEntries));

            List<string> res = new ();

            foreach (var strings in list!)
            {
                res.AddRange(strings);
            }

            return res.Distinct();
        });


    // получить данные о планете по url
    public async Task<Person?> GetPersonUrlAsync(string url)
    {
        var person = await GetDataAsync<Person>(url);

        if (person == null)
            return null;

        person.HomeWorld = (await GetDataAsync<Person>(person.HomeWorld ?? ""))?.Name;

        return person;
    }


    // получить данные обо всех персонажах
    public async Task<PeopleList?> GetPersonsAsync(int? maxCount = 42)
    {
        // запуск параллельного получение словаря с планетами (для улучшения производительности)
        var planetsTask = GetPlanetDictionaryAsync();

        // локальная функция для получения данных следующей страницы
        async Task<PeopleList?> GetNextData(PeopleList? peopleList) =>
            await GetDataAsync<PeopleList>(peopleList?.Next ?? "");

        // обнуление счётчика идентификаторов
        Person.CurrentMaxId = 0;

        // объект списка планет
        PeopleList? peopleList = await GetDataAsync<PeopleList>("https://swapi.dev/api/people/?format=json");

        if (peopleList == null)
            return null;

        // результат запроса
        PeopleList? result = peopleList;

        // чтение данных следующих информационных страниц
        do
        {
            result = await GetNextData(result);

            if (result?.Persons != null)
                peopleList.Persons?.AddRange(result.Persons);
        } while ((maxCount == null || peopleList.Persons?.Count < maxCount) && result?.Next != null);

        // удалить лишние элементы
        if (maxCount != null && peopleList?.Persons?.Count > maxCount)
            peopleList.Persons?.RemoveRange((int)maxCount, peopleList.Persons.Count - (int)maxCount);

        // ссылка на список персонажей (для улучшения производительности минуя многократный вызов геттера)
        var persons = peopleList?.Persons;

        // получение словаря планет
        var planets = planetsTask.Result;

        // установка названия планет
        for (int i = 0; i < persons?.Count; i++)
            persons[i].HomeWorld = planets?[persons[i].HomeWorld!].Name;

        return peopleList;
    }


    // получить данные обо всех планетах
    public async Task<PlanetList?> GetPlanetsAsync(int? maxCount = 33)
    {
        // локальная функция для получения данных следующей страницы
        async Task<PlanetList?> GetNextData(PlanetList? peopleList) =>
            await GetDataAsync<PlanetList>(peopleList?.Next ?? "");

        // обнуление счётчика идентификаторов
        Planet.CurrentMaxId = 0;

        // объект списка планет
        PlanetList? planetList = await GetDataAsync<PlanetList>("https://swapi.dev/api/planets/?format=json");

        if (planetList == null)
            return null;

        // результат запроса
        PlanetList? result = planetList;

        // чтение данных следующих информационных страниц
        do
        {
            result = await GetNextData(result);

            if (result?.Planets != null)
                planetList.Planets?.AddRange(result.Planets);
        } while ((maxCount == null || planetList.Planets?.Count < maxCount) && result?.Next != null);


        // удалить лишние элементы
        if (maxCount != null && planetList?.Planets?.Count > maxCount)
            planetList.Planets?.RemoveRange((int)maxCount, planetList.Planets.Count - (int)maxCount);

        return planetList;
    }

    #endregion
}
