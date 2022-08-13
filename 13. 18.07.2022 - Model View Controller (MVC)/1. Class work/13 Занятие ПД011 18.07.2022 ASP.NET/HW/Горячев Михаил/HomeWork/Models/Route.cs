namespace HomeWork.Models;

// Класс Маршрут
public class Route
{
    // идентификатор
    public int Id { get; set; }

    // страна
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    // цель поездки
    public int ObjectiveId { get; set; }
    public Objective? Objective { get; set; }

    // стоимость 1 дня пребывания
    public int DailyCost { get; set; }


    // поездки (отношение к "многим")
    public List<Visit>? Visits { get; set; }

    // строковое представление маршрута
    public string FullDataRoute => $"{Id}. {Country!.Name}, {Objective!.Name}";

    #region Конструкторы

    // конструктор по умолчанию
    public Route()
    {
    }


    // конструктор инициализирующий
    public Route(int id, Country? country, Objective? objective, int dailyCost)
    {
        Id = id;
        Country = country;
        Objective = objective;
        DailyCost = dailyCost;
    }

    // конструктор инициализирующий
    public Route(int id, int countryId, int objectiveId, int dailyCost)
    {
        Id = id;
        CountryId = countryId;
        ObjectiveId = objectiveId;
        DailyCost = dailyCost;
    }

    #endregion

    #region Методы

    // строковое представление объекта
    public override string ToString() =>
        $"{Id}. {Country!.Name}, {Objective!.Name}";

    #endregion
}
