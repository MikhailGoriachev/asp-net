using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

public class Country
{
    public int Id { get; set; }
    
    // название
    [Display(Name = "Название")]
    public string? Name { get; set; }
    
    // стоимость 1 дня пребывания
    [Display(Name = "Пребывание за день")]
    public int DailyCost { get; set; }
    
    // стоимость транспортных услуг 
    [Display(Name = "Транспортные услуги")]
    public int TransferCost { get; set; }
    
    // стоимость визы
    [Display(Name = "Стоимость визы")]
    public int VisaCost { get; set; }

    public List<Route> Routes { get; set; } = new();
}