using System.Diagnostics;
using HomeWork.Models;
using Newtonsoft.Json;

namespace HomeWork.Services;

// Сервис для получения данных
public class StartWarsService
{
    // получение данных по URL
    public async Task<T?> GetDataAsync<T>(string url) =>
        JsonConvert.DeserializeObject<T>(
            await (await new HttpClient().GetAsync(url)).Content.ReadAsStringAsync()
        );

    // получить данные обо всех персонажах
    public async Task<PeopleList?> GetPersonsAsync()
    {
        // локальная функция для получения данных следующей страницы
        async Task<PeopleList?> GetNextData(PeopleList? peopleList) => await GetDataAsync<PeopleList>(peopleList?.Next ?? "");

        // объект списка планет
        PeopleList? peopleList = await GetDataAsync<PeopleList>("https://swapi.dev/api/people/?format=json");

        if (peopleList == null)
            return null;

        // результат запроса
        PeopleList? result = peopleList;

        // чтение данных следующих информационных страниц
        while ((result = await GetNextData(result))?.Next != null)
        {
            if (result.Persons != null)
                peopleList.Persons?.AddRange(result.Persons);
        }

        return peopleList;
    }

    // получить данные обо всех планетах
    public async Task<PlanetList?> GetPlanetsAsync()
    {
        // локальная функция для получения данных следующей страницы
        async Task<PlanetList?> GetNextData(PlanetList? peopleList) => await GetDataAsync<PlanetList>(peopleList?.Next ?? "");

        // объект списка планет
        PlanetList? planetList = await GetDataAsync<PlanetList>("https://swapi.dev/api/planets/?format=json");

        if (planetList == null)
            return null;

        // результат запроса
        PlanetList? result = planetList;

        // чтение данных следующих информационных страниц
        while ((result = await GetNextData(result))?.Next != null)
        {
            if (result.Planets != null)
                planetList.Planets?.AddRange(result.Planets);
        }

        return planetList;
    }
}
