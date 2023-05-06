namespace HomeWork.Models.QueryResults;

// Класс Результат запроса 10
public class Query10Result
{
    // Выполняет группировку по единицам измерений проданных товаров, для каждой
    // единицы измерения вычисляет сумму продаж и суммарное количество проданного товара

    // единица измерения
    public Unit Unit { get; set; } = new Unit();

    // сумма продаж
    public int? Sum { get; set; }

    // количество
    public int? Amount { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public Query10Result()
    {

    }


    // конструктор инициализирующий
    public Query10Result(Unit unit, int? sum, int? amount)
    {
        Unit = unit;
        Sum = sum;
        Amount = amount;
    }

    #endregion
}
