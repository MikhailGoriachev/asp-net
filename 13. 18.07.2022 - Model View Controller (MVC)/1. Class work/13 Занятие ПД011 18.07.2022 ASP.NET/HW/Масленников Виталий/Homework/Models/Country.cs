namespace Homework.Models;

public class Country
{
    public int Id { get; set; }
    
    // название
    public string? Name { get; set; }
    
    // стоимость 1 дня пребывания
    public int DailyCost { get; set; }
    
    // стоимость транспортных услуг 
    public int TransferCost { get; set; }
    
    // стоимость визы
    public int VisaCost { get; set; }

    public List<Route> Routes { get; set; } = new();
}