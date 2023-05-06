using System.ComponentModel.DataAnnotations;

namespace Homework.Models.DTO;

// Модель для передачи данных о поездке
public class TravelDTO
{
   public int Id { get; set; }
   
   public int ClientId { get; set; }
   public string Client { get; set; }

   public string Passport { get; set; }

   public int CountryId { get; set; }
   public string Country { get; set; }

   public int PurposeId { get; set; }
   public string Purpose { get; set; }

   public int RouteId { get; set; }
   public int DailyCost { get; set; }

   public int TransferCost { get; set; }
   public int VisaCost { get; set; }

   public DateTime StartDate { get; set; }
   
   public int Duration { get; set; }

   public int FullCost { get; set; }


   public TravelDTO(Travel travel)
   {
      Id = travel.Id;
      ClientId = travel.ClientId;
      Client = travel.Client!.FullName;
      Passport = travel.Client.Passport!;
      CountryId = travel.Route!.CountryId;
      Country = travel.Route.Country!.Name!;
      PurposeId = travel.Route.PurposeId;
      Purpose = travel.Route.Purpose!.Name!;
      RouteId = travel.RouteId;
      DailyCost = travel.Route.DailyCost;
      TransferCost = travel.Route.Country.TransferCost;
      VisaCost = travel.Route.Country.VisaCost;
      StartDate = travel.StartDate;
      Duration = travel.Duration;
      FullCost = (int)Math.Ceiling((travel.Duration * (travel.Route!.DailyCost + travel.Route!.Country!.DailyCost)
                               + travel.Route.Country.TransferCost + travel.Route.Country.VisaCost) * 1.03);
   }
}