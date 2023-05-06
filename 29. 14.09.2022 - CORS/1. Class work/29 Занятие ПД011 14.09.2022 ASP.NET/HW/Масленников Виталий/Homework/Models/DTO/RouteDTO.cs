using System.ComponentModel.DataAnnotations;

namespace Homework.Models.DTO;

// Модель для передачи данных о маршруте
public class RouteDTO
{
    public int Id { get; set; }
    
    public int PurposeId { get; set; }
    
    public string Purpose { get; set; }
    
    public int CountryId { get; set; }
    
    public string Country { get; set; }
    
    public int VisaCost { get; set; }
    
    public int TransferCost { get; set; }

    public int DailyCost { get; set; }

    public RouteDTO(Route route)
    {
        Id = route.Id;
        PurposeId = route.PurposeId;
        Purpose = route.Purpose!.Name!;
        CountryId = route.CountryId;
        Country = route.Country!.Name!;
        VisaCost = route.Country!.VisaCost;
        TransferCost = route.Country.TransferCost;
        DailyCost = route.DailyCost + route.Country.DailyCost;
    }
}