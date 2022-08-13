namespace HomeWork.Models;

// Класс Цель поездки
public class Objective
{
    // идентификатор
    public int Id { get; set; }

    // название
    public string? Name { get; set; }


    // маршруты (отношение к "многим")
    public List<Route>? Routes { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Objective()
    {
    }


    // конструктор инициализирующий
    public Objective(int id, string? name)
    {
        Id = id;
        Name = name;
    }

    #endregion
}
