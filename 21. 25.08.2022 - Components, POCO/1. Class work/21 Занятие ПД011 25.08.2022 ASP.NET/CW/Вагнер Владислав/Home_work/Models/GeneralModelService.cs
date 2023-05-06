using Newtonsoft.Json;
using Home_work.Models.Characters;

namespace Home_work.Models;
public class GeneralModelService
{
    private string _postsUrl = "https://jsonplaceholder.typicode.com/users/2/posts";

    private HttpClient httpClient;

    public GeneralModelService()
    {
        httpClient = new HttpClient();
    }

    //Получение персонажей
    public async Task<CharactersDeserializationModel> GetCharactersAsync(
        string characterUrl = "https://swapi.dev/api/people/?format=json")
    {
        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(characterUrl);

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<CharactersDeserializationModel>(json);
    }

    //Получение конкретного перснажа
    public async Task<Character> GetCharacterAsync(string url)
    {

        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(url);

        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Character>(json);
    }

    //Получение постов пользователей
    public async Task<List<Planet>> GetPostsAsync()
    {

        //Получение данных от сервера
        HttpResponseMessage response = await httpClient.GetAsync(_postsUrl);


        string json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Planet>>(json);
    }

}
