namespace Homework.Models.DTO;

// Модель для передачи данных о закупке (Data Transfer Object)
public class PurchaseDTO
{
    public int Id { get; set; }
    
    public string Goods { get; set; }
    
    public DateTime PurchaseDate { get; set; }

    public int Amount { get; set; }
    
    public string Unit { get; set; }

    public int Price { get; set; }

    public PurchaseDTO(Purchase purchase)
    {
        Id = purchase.Id;
        Goods = purchase.Goods!.Name;
        PurchaseDate = purchase.PurchaseDate;
        Amount = purchase.Amount;
        Unit = purchase.Unit!.Short;
        Price = purchase.Price;
    }
}