using Homework.Models;
using Newtonsoft.Json;

namespace Homework.Services;

// Сервис поставляющий данные от внешнего источника
public class UserService
{
    // URL сервиса поставки данных - API
    private const string Url = "https://jsonplaceholder.typicode.com/";

    public async Task<List<User>> GetUsersAsync() => 
        await GetUserDataAsync<User>($"{Url}users");

    public async Task<List<Post>> GetUserPostsAsync(int id) => 
        await GetUserDataAsync<Post>($"{Url}users/{id}/posts");

    public async Task<List<Photo>> GetUserPhotosAsync(int id) => 
        await GetUserDataAsync<Photo>($"{Url}albums/{id}/photos");

    public async Task<List<UserTask>> GetUserTasksAsync(int id) => 
        await GetUserDataAsync<UserTask>($"{Url}users/{id}/todos");

    private async Task<List<T>> GetUserDataAsync<T>(string url = Url)
    {
        HttpClient http = new HttpClient();
        HttpResponseMessage response = await http.GetAsync(url);
        
        string content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<List<T>>(content)!;
    }
    
}