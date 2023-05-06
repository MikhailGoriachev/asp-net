namespace UsingTagHelpers.Models;

/// <summary>
/// Товар	
/// *Наименование товара
/// </summary>
public class Good
{
    public int Id { get; set; }

    // Наименование товара
    public string? NameGood { get; set; }

    public List<Purchase> Purchases { get; set; } = new();
    //public List<Sale> Sales { get; set; } = new();
}
