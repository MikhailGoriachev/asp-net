namespace HomeWork.Models;

// Класс Клиент
public class Client
{
    // идентификатор
    public int Id { get; set; }

    // фамилия
    public string? Surname { get; set; }

    // имя
    public string? Name { get; set; }

    // отчество
    public string? Patronymic { get; set; }

    // паспорт
    public string? Passport { get; set; }


    // поездки (отношение к "многим")
    public List<Visit>? Visits { get; set; }

    // строковое представление клиента
    public string FullDataClient => $"{Surname!} {Name} {Patronymic} {Passport}";

    #region Констуркторы

    // конструктор по умолчанию
    public Client()
    {
    }


    // конструктор инициализирущий
    public Client(int id, string? surname, string? name, string? patronymic, string? passport)
    {
        Id = id;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Passport = passport;
    }

    #endregion
}
