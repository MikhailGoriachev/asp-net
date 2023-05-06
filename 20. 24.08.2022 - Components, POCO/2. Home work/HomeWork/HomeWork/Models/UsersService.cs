using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Сервис данных пользователей
public class UsersService
{
    // адрес ресурса
    public string SourceUrl { get; set; } = "https://jsonplaceholder.typicode.com";


    #region Методы

    // получение коллекции пользователей
    public async Task<IEnumerable<User>?> GetUsersAsync() =>
        await GetDataUrlAsync<IEnumerable<User>>($"{SourceUrl}/users/");


    // получение коллекции постов пользователя по ID
    public async Task<IEnumerable<UserPost>?> GetUserPostsAsync(int id) =>
        await GetDataUrlAsync<IEnumerable<UserPost>>($"{SourceUrl}/users/{id}/posts");


    // получение коллекции фото пользователя по ID
    public async Task<IEnumerable<UserPhoto>?> GetUserPhotosAsync(int id) =>
        await GetDataUrlAsync<IEnumerable<UserPhoto>>($"{SourceUrl}/albums/{id}/photos");


    // получение коллекции дел пользователя по ID
    public async Task<IEnumerable<UserTodo>?> GetUserTodosAsync(int id) =>
        await GetDataUrlAsync<IEnumerable<UserTodo>>($"{SourceUrl}/users/{id}/todos");


    // запрос по URL
    public async Task<T?> GetDataUrlAsync<T>(string url) =>
        JsonConvert.DeserializeObject<T>(
            await (await new HttpClient().GetAsync(url)).Content.ReadAsStringAsync()
        );

    #endregion
}
