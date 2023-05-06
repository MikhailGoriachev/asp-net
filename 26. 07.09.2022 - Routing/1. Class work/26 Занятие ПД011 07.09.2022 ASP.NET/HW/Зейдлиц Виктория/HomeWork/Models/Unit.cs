namespace HomeWork.Models;

/// <summary>
/// Единица измерения товара	
/// *Единица измерения товара
/// </summary>
public class Unit
{
    public int Id { get; set; }

    // Краткое наименование единицы измерения
    public string? Short { get; set; }
    // Полное наименование единицы измерения
    public string? Long { get; set; }

    public List<Purchase> Purchases { get; set; } = new();


}
