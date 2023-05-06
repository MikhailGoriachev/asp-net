using Newtonsoft.Json;

namespace HomeWork.Models.Task2;

// Сервис поставляющий данные от внешнего источника
public class PlanetService
{
    // URL сервиса поставки данных - API
    private string url = "https://swapi.dev/api/planets/";

    // получить данные асинхронно
    public async Task<List<Planet>> GetPlanetsAsync()
    {
        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(url);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();
        PlanetJson planetJson = JsonConvert.DeserializeObject<PlanetJson>(content)!;

        return planetJson.Results!.ToList();
    } // GetPlanetsAsync

} // class PlanetService
