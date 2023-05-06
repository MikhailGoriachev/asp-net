using Newtonsoft.Json;

namespace Home_work.Models;

public class GeneralModelService
{
    private string _usersUrl = "https://jsonplaceholder.typicode.com/users/";
    private string _postsUrl = "https://jsonplaceholder.typicode.com/users/2/posts";
    private string _albumUrl = "https://jsonplaceholder.typicode.com/albums/1/photos";
    private string _todoUrl = "https://jsonplaceholder.typicode.com/users/3/todos";

    private HttpClient httpClient;

    public GeneralModelService()
    {
        httpClient = new HttpClient();
    }

    //Получение пользователей
    public async Task<List<User>> GetUsersAsync()
    {
        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(_usersUrl);

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<User>>(json);
    }

    //Получение постов пользователей
    public async Task<List<Post>> GetPostsAsync()
    {

        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(_postsUrl);


        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Post>>(json);
    }

    //Получение альбома пользователей
    public async Task<List<Photo>> GetAlbumAsync()
    {

        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(_albumUrl);

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Photo>>(json);
    }

    //Получение списка дель пользователя
    public async Task<List<Todo>> GetTodoAsync()
    {
        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(_todoUrl);

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Todo>>(json);
    }
}
