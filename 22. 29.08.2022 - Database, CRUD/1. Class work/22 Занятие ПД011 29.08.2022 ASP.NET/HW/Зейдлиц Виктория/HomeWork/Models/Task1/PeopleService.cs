using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeWork.Models.Task1;


// Сервис поставляющий данные от внешнего источника
public class PeopleService
{
    // URL сервиса поставки данных - API
    private string url = "https://swapi.dev/api/people/";

    // получить данные асинхронно
    public async Task<List<Person>> GetPeoplesAsync()
    {
        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(url);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();
        PeopleJson peopleJson = JsonConvert.DeserializeObject<PeopleJson>(content)!;

        return peopleJson.Results!.ToList();
    } // GetPeoplesAsync


    public async Task<List<Person>> GetPeoplesAsync(int id)
    {
        string urlId = $"https://swapi.dev/api/people/{id}/";

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(urlId);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();
        PeopleJson peopleJson = JsonConvert.DeserializeObject<PeopleJson>(content)!;

        return peopleJson.Results!.ToList();
    } // GetPeoplesAsync


    public async Task<List<Person>> GetPeoplesOrderedAsync()
    {

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(url);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();
        PeopleJson peopleJson = JsonConvert.DeserializeObject<PeopleJson>(content)!;

        return peopleJson.Results!.OrderByDescending(p => p.Mass).ToList();
    } // GetPeoplesOrderedAsync


    public async Task<List<Person>> GetPeoplesSelectedMassAsync(string from = "32", string to = "75")
    {

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(url);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();
        PeopleJson peopleJson = JsonConvert.DeserializeObject<PeopleJson>(content)!;

        return peopleJson.Results!.Where(p => p.Mass == from && p.Mass == to).ToList();
    } // GetPeoplesOrderedAsync


} // class PeopleService
