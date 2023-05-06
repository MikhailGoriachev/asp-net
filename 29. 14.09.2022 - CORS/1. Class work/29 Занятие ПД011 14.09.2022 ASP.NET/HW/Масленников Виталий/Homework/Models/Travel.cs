using System.ComponentModel.DataAnnotations;

namespace Homework.Models;

public class Travel
{
   public int Id { get; set; }
   
   // Клиент
   [Display(Name = "Клиент")]
   public int ClientId { get; set; }
   public Client? Client { get; set; }
     
   // Маршрут
   [Display(Name = "Маршрут")]
   public int RouteId { get; set; }
   public Route? Route { get; set; }
   
   // Дата начала поездки
   [Display(Name = "Дата начала поездки")]   
   public DateTime StartDate { get; set; }

   // Длительность поездки 
   [Display(Name = "Длительность поездки")]
   public int Duration { get; set; }
}