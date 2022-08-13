namespace Homework.Models;

public class Travel
{
   public int Id { get; set; }
   
   // Клиент
   public int ClientId { get; set; }
   public Client? Client { get; set; }
     
   // Маршрут
   public int RouteId { get; set; }
   public Route? Route { get; set; }
   
   // Дата начала поездки
   public DateTime StartDate { get; set; }
   
   /*
   // Полная стоимость
   public int TotalCost =>
      (int)((Duration * (Route!.DailyCost + Route!.Country!.DailyCost) + Route.Country.TransferCost + Route.Country.VisaCost) *
      1.03);
      */
   
   // Длительность поездки 
   public int Duration { get; set; }
}