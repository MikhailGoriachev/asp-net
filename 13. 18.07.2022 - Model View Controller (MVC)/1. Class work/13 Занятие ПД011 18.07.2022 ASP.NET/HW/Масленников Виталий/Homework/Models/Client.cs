namespace Homework.Models;

public class Client
{
    public int Id { get; set; }
    
    // Фамилия
    public string? Surname { get; set; }
    // Имя
    public string? Name { get; set; }
    // Отчество
    public string? Patronymic { get; set; }
    // Серия паспорта
    public string? Passport { get; set; }


    public string FullName => $"{Surname} {Name?.First()}.{Patronymic?.First()}.";

    public List<Travel> Trips { get; set; } = new();
}