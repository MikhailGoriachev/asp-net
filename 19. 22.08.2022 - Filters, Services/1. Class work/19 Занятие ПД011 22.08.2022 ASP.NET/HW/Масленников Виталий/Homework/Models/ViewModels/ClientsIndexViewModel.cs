using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Models.ViewModels;

public class ClientsIndexViewModel
{
    public IEnumerable<TravelCompany.Client> Clients { get; set; } = null!;
    
    // Минимальный возраст для фильтра
    [Display(Name = "от:")]
    [Required(ErrorMessage = "Введите минимальный возраст")]
    [Range(18,65, ErrorMessage = "Введите значение минимального возраста от 18 до 65")] 
    public int AgeMin { get; set; }
    
    // Максимальный возраст для фильтра
    [Display(Name = "до:")]
    [Required(ErrorMessage = "Введите максимальный возраст")]
    [Range(18,65, ErrorMessage = "Введите значение максимального возраста от 18 до 65")] 
    public int AgeMax { get; set; }

    // Фамилия для фильтра
    [Display(Name = "Фамилия:")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    public string Surname { get; set; } = "";

    // Список для выбора типа клиентов
    [Display(Name = "Тип клиента:")]
    public SelectList RegularityOptions { get; set; } = new(new List<SelectListItem>
    {
        new() {Text = "Постоянный", Value = "regular"},
        new() {Text = "Не постоянный", Value = "irregular"}
    }, "Value", "Text");
}