namespace HomeWork.Models;

// Класс Страна
public class Country
{
    // идентификатор
    public int Id { get; set; }

    // название
    public string? Name { get; set; }

    // стоимость транспортных услуг
    public int CostService { get; set; }

    // стоимость оформления визы
    public int CostVisa { get; set; }


    // маршруты (отношение к "многим")
    public List<Route>? Routes { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Country()
    {
    }


    // конструктор инициализирующий
    public Country(int id, string? name, int costService, int costVisa)
    {
        Id = id;
        Name = name;
        CostService = costService;
        CostVisa = costVisa;
    }

    #endregion
}
