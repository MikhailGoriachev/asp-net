using Homework.Models.TravelCompany;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Models.ViewModels;

public class RoutesIndexViewModel
{
    public IEnumerable<TravelCompany.Route> Routes { get; set; } = null!;
    public IEnumerable<Instructor> Instructors { get; set; } = null!;

    public PointsSelectLists PointsSelectLists { get; set; } = null!;

    public string GetInstructorName(int id) => 
        Instructors.FirstOrDefault(i => i.Id == id)!.ShortName;
}