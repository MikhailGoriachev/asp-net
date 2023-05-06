using System.ComponentModel.DataAnnotations;

namespace Homework.Models.DTO;

// Модель для передачи данных о клиенте
public class ClientDTO
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

    public ClientDTO(Client client)
    {
        Id = client.Id;
        Surname = client.Surname;
        Name = client.Name;
        Patronymic = client.Patronymic;
        Passport = client.Passport;
    }
}