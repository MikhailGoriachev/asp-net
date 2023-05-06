using Homework.Models.TravelCompany;
using Microsoft.AspNetCore.Mvc.Rendering;
using Route = Homework.Models.TravelCompany.Route;

namespace Homework.Models.ViewModels;

// Модель представления для формы ввода данных о маршруте
public class RouteInputForm
{
    public RouteInputForm(Route route, IEnumerable<Instructor> instructors, IEnumerable<string> difficulties)
    {
        InstructorsList = new SelectList(instructors, "Id", "ShortName");
        DifficultiesList = new SelectList(difficulties);
        Route = route; 
    }

    // Объект для вводимых данных
    public Route Route { get; set; }
    
    // Список инструкторов
    public SelectList InstructorsList { get; set; }
    
    // Список категорий сложности
    public SelectList DifficultiesList { get; set; }
}