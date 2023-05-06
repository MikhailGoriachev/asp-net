using System.ComponentModel.DataAnnotations;
using Homework.Models.TravelCompany;

namespace Homework.Models.ViewModels;

public class RoutesIndexViewModel
{
    public IEnumerable<TravelCompany.Route> Routes { get; set; } = null!;
    public IEnumerable<Instructor> Instructors { get; set; } = null!;
    public PointsSelectLists PointsSelectLists { get; set; } = null!;

    [Display(Name = "до:")]
    [Required(ErrorMessage = "Введите протяженность маршрута")]
    [Range(1,4000, ErrorMessage = "Введите значение значение конечной протяженности от 1 до 4000")] 
    public int RouteMaxLength { get; set; }
    
    [Display(Name = "от:")]
    [Required(ErrorMessage = "Введите протяженность маршрута")]
    [Range(1,4000, ErrorMessage = "Введите значение начальной протяженности от 1 до 4000")] 
    public int RouteMinLength { get; set; }

    public string GetInstructorName(int id) => 
        Instructors.FirstOrDefault(i => i.Id == id)!.ShortName;
}