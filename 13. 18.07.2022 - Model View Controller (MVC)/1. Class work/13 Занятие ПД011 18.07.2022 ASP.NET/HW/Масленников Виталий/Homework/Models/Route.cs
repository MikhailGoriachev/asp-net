namespace Homework.Models;

public class Route
{
    public int Id { get; set; }
    
    //Страна
    public int CountryId { get; set; }
    public Country? Country { get; set; }
    
    //Цель поездки
    public int PurposeId { get; set; }
    public Purpose? Purpose { get; set; }
    
    //Стоимость 1 дня пребывания
    public int DailyCost { get; set; }

    public List<Travel> Travels { get; set; } = new();
}