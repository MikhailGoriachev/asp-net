namespace HomeWork.Models.Queries;

// Результат запроса 11
// *ФИО продавца
// *Сумма продаж продавца
// *Количество продаж продавца
// *Минимальная стоимость продаж
// *Максимальная стоимость продаж 
public class Query11Result
{
    // продавец
    public Seller Seller { get; set; } = new Seller();

    // сумма продаж
    public int? SumSales { get; set; }

    // количество продаж
    public int? AmountSales { get; set; }

    // минимальная цена продажи единицы товара
    public int? MinPrice { get; set; }

    // максимальная цена продажи единицы товара
    public int? MaxPrice { get; set; }


    // Конструкторы
    public Query11Result()
    {
    }

    public Query11Result(Seller seller, int? minPrice, int? sumSale, int? maxPrice, int? amountSales)
    {
        Seller = seller;
        MinPrice = minPrice;
        SumSales = sumSale;
        MaxPrice = maxPrice;
        AmountSales = amountSales;
    }

} // class Query11Result
