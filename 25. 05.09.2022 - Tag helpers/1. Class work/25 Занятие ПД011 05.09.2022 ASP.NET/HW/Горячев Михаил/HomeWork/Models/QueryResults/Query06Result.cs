namespace HomeWork.Models.QueryResults;

// Класс Результат запроса 6
public class Query06Result
{
    // Дата продажи
    public DateTime DateSale { get; set; }

    // Наименование товара
    public string GoodsName { get; set; } = "";

    // Цена закупки
    public int PurchasePrice { get; set; }

    // Цена продажи
    public int SalePrice { get; set; }

    // Количество проданных единиц
    public int Amount { get; set; }
    
    // Прибыль
    public int Profit { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public Query06Result()
    {

    }

    // конструктор инициализирующий
    public Query06Result(DateTime dateSale, string goodsName, int purchasePrice, int salePrice, int amount, int profit)
    {
        DateSale = dateSale;
        GoodsName = goodsName;
        PurchasePrice = purchasePrice;
        SalePrice = salePrice;
        Amount = amount;
        Profit = profit;
    }

    #endregion
}
