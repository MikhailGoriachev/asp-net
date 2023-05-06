using System.ComponentModel.DataAnnotations;

namespace Homework.Models.TravelCompany;

// Туристический маршрут
public class Route
{
    [Display(Name = "Id")]
    public int Id { get; set; }                         // Идентификатор

    [Display(Name = "Начальный пункт:")]
    [Required(ErrorMessage = "Введите начальный пункт маршрута")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string StartPoint { get; set; }  = null!;     // Начальный пункт маршрута

    [Display(Name = "Промежуточный пункт:")]
    [Required(ErrorMessage = "Введите промежуточный пункт маршрута")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string MiddlePoint { get; set; }  = null!;   // Обязательный промежуточный пункт маршрута

    [Display(Name = "Конечный пункт:")]
    [Required(ErrorMessage = "Введите конечный пункт маршрута")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string FinishPoint { get; set; }   = null!;  // Конечный пункт маршрута

    [Display(Name = "Протяженность, км:")]
    [Required(ErrorMessage = "Введите протяженность маршрута")]
    [Range(1,4000, ErrorMessage = "Введите значение от 1 до 4000")] 
    public int Length { get; set; }                     // Протяженность маршрута в км. (целое число)

    [Display(Name = "Сложность:")]
    [Required(ErrorMessage = "Выберите сложность маршрута")]
    public string Difficulty { get; set; }    = null!;  // Сложность маршрута

    [Display(Name = "Инструктор:")]
    [Required(ErrorMessage = "Выберите инструктора маршрута")]
    public int InstructorId { get; set; }
}