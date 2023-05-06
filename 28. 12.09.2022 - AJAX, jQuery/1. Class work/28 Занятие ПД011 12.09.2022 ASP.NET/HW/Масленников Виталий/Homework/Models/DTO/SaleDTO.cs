namespace Homework.Models.DTO;

// Модель для передачи данных о продаже (Data Transfer Object)
public class SaleDTO
{
    public int Id { get; set; }
    
    public DateTime SaleDate { get; set; }
    
    public string Goods { get; set; }
    
    public int UnitId { get; set; }
    public string Unit { get; set; }
    
    public int SellerId { get; set; }
    
    public string Seller { get; set; }
    
    public int SellPrice { get; set; }
    
    public int PurchaseId { get; set; }
    public int PurchasePrice { get; set; }
    public int Amount { get; set; }

    public SaleDTO(Sale sale)
    {
        Id = sale.Id;
        SaleDate = sale.SaleDate;
        Goods = sale.Purchase!.Goods!.Name;
        SellerId = sale.SellerId;
        Seller = sale.Seller!.ShortName;
        Amount = sale.Amount;
        PurchasePrice = sale.Purchase!.Price;
        PurchaseId = sale.PurchaseId;
        SellPrice = sale.Price;
        UnitId = sale.UnitId;
        Unit = sale.Unit!.Short;
    }
}