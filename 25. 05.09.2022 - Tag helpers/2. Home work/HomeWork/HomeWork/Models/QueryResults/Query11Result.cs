namespace HomeWork.Models.QueryResults;

// Класс Результат запроса 11
public class Query11Result
{
    // Для всех продавцов вывести сумму и количество продаж, минимальную и максимальную стоимости продаж

    // продавец
    public Seller Seller { get; set; } = new Seller();

    // минимальная цена продажи единицы товара
    public int? MinPrice { get; set; }

    // средняя цена продажи единицы товара
    public int? AvgPrice { get; set; }

    // максимальная цена продажи единицы товара
    public int? MaxPrice { get; set; }

    // сумма проданных товаров
    public int SumCost { get; set; }

    // количество продаж
    public int? AmountSales { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Query11Result()
    {

    }


    // конструктор инициализирующий
    public Query11Result(Seller seller, int? minPrice, int? avgPrice, int? maxPrice, int? amountSales)
    {
        Seller = seller;
        MinPrice = minPrice;
        AvgPrice = avgPrice;
        MaxPrice = maxPrice;
        AmountSales = amountSales;
    }

    #endregion
}
