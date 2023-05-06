using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Список персонажей
public class PeopleList
{
    // количество
    public int Count { get; set; }

    // следующая страница с данными
    public string? Next { get; set; }

    // результат (персонажи)
    [JsonProperty("results")]
    public List<Person>? Persons { get; set; }
}
