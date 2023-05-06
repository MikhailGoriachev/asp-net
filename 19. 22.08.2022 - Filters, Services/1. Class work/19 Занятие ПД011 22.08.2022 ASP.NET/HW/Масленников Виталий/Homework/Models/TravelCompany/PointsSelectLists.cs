using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Models.TravelCompany;

// Списки для выбора пунктов маршрута
public class PointsSelectLists
{
    public SelectList StartPoints { get; set; } = null!;
    public SelectList MiddlePoints { get; set; } = null!;
    public SelectList FinishPoints { get; set; } = null!;
}