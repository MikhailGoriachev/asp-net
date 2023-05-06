using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncComponents.Model;

// Сервис поставляющий данные от внешнего источника
public class UserService
{
    // URL сервиса поставки данных - API
    private string url = "https://jsonplaceholder.typicode.com/users";

    // получить данные асинхронно
    public async Task<List<User>> GetUsersAsync() {

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(url);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();

        // распарсить коллекцию из строки 
        return JsonConvert.DeserializeObject<List<User>>(content)!;
    } // GetUsersAsync
} // class UserService

