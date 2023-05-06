namespace EntityFrameworkInAspNetCoreMvcIntro.Models;

// Заданные режимы сортировки для 
//     по дате начала
//     по длительности
//     по стране пребывания
public enum SortState
{
    StartAsc,      //по дате начала по возрастанию
    StartDesc,     // по дате начала по убыванию
    DurationAsc,  // по длительности по возрастанию
    DurationDesc, // по длительности по убыванию
    CountryAsc,    // по стране пребывания по возрастанию
    CountryDesc    // по стране пребывания по убыванию
}

