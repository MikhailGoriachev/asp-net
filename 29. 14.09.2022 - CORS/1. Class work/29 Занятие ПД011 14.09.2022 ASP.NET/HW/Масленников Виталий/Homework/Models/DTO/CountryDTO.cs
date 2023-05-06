using System.ComponentModel.DataAnnotations;

namespace Homework.Models.DTO;

// Модель для передачи данных о стране
public class CountryDTO
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

    public CountryDTO(Country country)
    {
        Id = country.Id;
        Name = country.Name;
        DailyCost = country.DailyCost;
        TransferCost = country.TransferCost;
        VisaCost = country.VisaCost;
    }
}