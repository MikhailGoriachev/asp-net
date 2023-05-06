namespace HomeWork.Models.QueryResults;

// Класс Результат запроса 9
public class Query09Result
{
    // продавец
    public Seller Seller { get; set; } = new Seller();

    // минимальная цена продажи единицы товара
    public int? MinPrice { get; set; }

    // средняя цена продажи единицы товара
    public int? AvgPrice { get; set; }

    // максимальная цена продажи единицы товара
    public int? MaxPrice { get; set; }

    // количество продаж
    public int? AmountSales { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Query09Result()
    {

    }


    // конструктор инициализирующий
    public Query09Result(Seller seller, int? minPrice, int? avgPrice, int? maxPrice, int? amountSales)
    {
        Seller = seller;
        MinPrice = minPrice;
        AvgPrice = avgPrice;
        MaxPrice = maxPrice;
        AmountSales = amountSales;
    }

    #endregion
}
