using Newtonsoft.Json;

namespace HomeWork.Models;

public class UsersService
{
    // адрес ресурса
    public string SourceUrl { get; set; } = "https://jsonplaceholder.typicode.com";


    #region Методы

    // получение коллекции пользователей
    public async Task<IEnumerable<User>?> GetUsersAsync() =>
        JsonConvert.DeserializeObject<IEnumerable<User>>(
            await (await new HttpClient().GetAsync($"{SourceUrl}/users/")).Content.ReadAsStringAsync()
        );
    // public async Task<IEnumerable<User>?> GetUsersAsync()
    // {
    //     // var res = await new HttpClient().GetAsync($"{SourceUrl}/users");
    //     var res = await new HttpClient().GetAsync($"{SourceUrl}/users/");
    //
    //     return JsonConvert.DeserializeObject<IEnumerable<User>>(
    //         await res.Content.ReadAsStringAsync()
    //     );
    // }

    // получение коллекции постов пользователя по ID
    public async Task<IEnumerable<UserPost>?> GetUserPostsAsync(int id) =>
        JsonConvert.DeserializeObject<IEnumerable<UserPost>>(
            await (await new HttpClient().GetAsync($"{SourceUrl}/users/{id}/posts")).Content.ReadAsStringAsync()
        );

    // получение коллекции фото пользователя по ID
    public async Task<IEnumerable<UserPhoto>?> GetUserPhotosAsync(int id) =>
        JsonConvert.DeserializeObject<IEnumerable<UserPhoto>>(
            await (await new HttpClient().GetAsync($"{SourceUrl}/albums/{id}/photos")).Content.ReadAsStringAsync()
        );

    // получение коллекции дел пользователя по ID
    public async Task<IEnumerable<UserTodo>?> GetUserTodosAsync(int id) =>
        JsonConvert.DeserializeObject<IEnumerable<UserTodo>>(
            await (await new HttpClient().GetAsync($"{SourceUrl}/users/{id}/todos")).Content.ReadAsStringAsync()
        );

    #endregion




}
