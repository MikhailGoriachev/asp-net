using System.ComponentModel.DataAnnotations;

namespace Homework.Models.TravelCompany;

// Туристический маршрут
public class Route
{
    [Display(Name = "Id")]
    public int Id { get; set; }                         // Идентификатор

    [Display(Name = "Начальный пункт")]
    public string StartPoint { get; set; }  = null!;     // Начальный пункт маршрута

    [Display(Name = "Промежуточный пункт")]
    public string MiddlePoint { get; set; }  = null!;   // Обязательный промежуточный пункт маршрута

    [Display(Name = "Конечный пункт")]
    public string FinishPoint { get; set; }   = null!;  // Конечный пункт маршрута

    [Display(Name = "Протяженность, км")]
    public int Length { get; set; }                     // Протяженность маршрута в км. (целое число)

    [Display(Name = "Сложность")]
    public string Difficulty { get; set; }    = null!;  // Сложность маршрута

    [Display(Name = "Инструктор")]
    //public string InstructorName { get; set; } = null!; 

    public int InstructorId { get; set; }
}