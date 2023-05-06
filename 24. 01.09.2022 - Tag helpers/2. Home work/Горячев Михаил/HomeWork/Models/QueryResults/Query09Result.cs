namespace HomeWork.Models.QueryResults;

// Класс Результат запроса 9
public class Query09Result
{
    // название товара
    public string GoodsName { get; set; } = "";

    // минимальная цена закупки
    public int? MinPrice { get; set; }

    // средняя цена закупки
    public int? AvgPrice { get; set; }

    // максимальная цена закупки
    public int? MaxPrice { get; set; }

    // количество закупок
    public int? Amount { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Query09Result()
    {

    }


    // конструктор инициализирующий
    public Query09Result(string goodsName, int? minPrice, int? avgPrice, int? maxPrice, int? amount)
    {
        GoodsName = goodsName;
        MinPrice = minPrice;
        AvgPrice = avgPrice;
        MaxPrice = maxPrice;
        Amount = amount;
    }

    #endregion
}
