using Newtonsoft.Json;

namespace ViewComponentsFirst.Model;

// Сервис поставляющий данные от внешнего источника
public class UserService
{
    // получить данные пользователя асинхронно
    public async Task<List<User>> GetUsersAsync() {
        // URL сервиса поставки данных - API
        string urlUsers = "https://jsonplaceholder.typicode.com/users";

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(urlUsers);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();

        // распарсить коллекцию из строки 
        return JsonConvert.DeserializeObject<List<User>>(content)!;
    } // GetUsersAsync


    // получить посты пользователя с идентфикатором id асинхронно
    public async Task<List<Post>> GetUserPostsAsync(int id) {
        // URL сервиса поставки данных - API
        string urlPosts = $"https://jsonplaceholder.typicode.com/users/{id}/posts";

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(urlPosts);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();

        // распарсить коллекцию из строки 
        return JsonConvert.DeserializeObject<List<Post>>(content)!;
    } // GetUserPostsAsync


    // получить фотографии пользователя с идентфикатором id асинхронно
    public async Task<List<Photo>> GetUserPhotosAsync(int id) {
        // URL сервиса поставки данных - API
        string urlPhotos = $"https://jsonplaceholder.typicode.com/albums/{id}/photos";

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(urlPhotos);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();

        // распарсить коллекцию из строки 
        return JsonConvert.DeserializeObject<List<Photo>>(content)!;
    } // GetUserPhotosAsync


    // получить список запланированных дел пользователя с идентфикатором id асинхронно
    public async Task<List<Todo>> GetUserTodosAsync(int id) {
        // URL сервиса поставки данных - API
        string urlTodos = $"https://jsonplaceholder.typicode.com/users/{id}/todos";

        // асинхронная отправка запроса и ожидание ответа, в другом потоке
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(urlTodos);

        // чтение данных из ответа сервера - тоже асинхронно, в другом потоке 
        string content = await response.Content.ReadAsStringAsync();

        // распарсить коллекцию из строки 
        return JsonConvert.DeserializeObject<List<Todo>>(content)!;
    } // GetUserTodosAsync


} // class UserService

