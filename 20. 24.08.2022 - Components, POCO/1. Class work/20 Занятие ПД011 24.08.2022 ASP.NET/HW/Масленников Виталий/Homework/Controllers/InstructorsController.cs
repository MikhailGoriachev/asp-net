using Homework.Common;
using Homework.Infrastructure;
using Homework.Models.TravelCompany;
using Homework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Controllers;

[Route("Instructors")]
public class InstructorsController : Controller
{
    private TravelCompany _travelCompany;

    // Конструктор
    public InstructorsController(TravelCompany travelCompany) => 
        _travelCompany = travelCompany;
    
    public IActionResult ExceptionTest() =>
        throw new Exception($"{ControllerContext.RouteData.Values["controller"]} error");
    
    // Вывод сведений об инструкторах в порядке хранения в файле
    [HttpGet("Index")]
    public IActionResult Index()
    {
        ViewBag.Categories = new SelectList(Common.Seed.Categories); 
        return View(_travelCompany.Instructors);
    }
    
    // Вывод сведений об инструкторах в порядке, заданном параметрами 
    [HttpGet("OrderBy/{property}/{sort}")]
    public IActionResult OrderBy(string property, string sort)
    {
        ViewBag.Categories = new SelectList(Common.Seed.Categories);

        var instructors =  (property == "Category") 
            ? _travelCompany.Instructors.OrderByDescending(i => Seed.Categories.IndexOf(i.Category))
            : _travelCompany.Instructors.OrderBySort(property, sort);;
        
        return View("Index", instructors);
    }
    
    //	Вывод сведений об инструкторах с заданной категорией
    [HttpPost("WhereCategory")]
    public IActionResult WhereCategory(string category)
    {
        ViewBag.Categories = new SelectList(Seed.Categories); 
        return View("Index", _travelCompany.Instructors.Where(i => i.Category == category));
    }
    
    // Переход на форму редактирования данных инструктора
    [HttpGet("EditInstructor/{id:int}")]
    public IActionResult EditInstructor(int id)
    {
        ViewBag.Categories = new SelectList(Seed.Categories);
        var instructor = _travelCompany.Instructors.First(b => b.Id == id);
        return View("InstructorForm", instructor);
    }
    
    // Обновить данные об инструкторе
    [HttpPost("UpdateInstructor")]
    public IActionResult UpdateInstructor(Instructor instructor)
    {
        if(!ModelState.IsValid)
            return View("Index", _travelCompany.Instructors);
        
        _travelCompany.AddOrUpdateInstructor(instructor);
        _travelCompany.SaveData();
        return View("Index", _travelCompany.Instructors);
    }
    
    // Получить данные об инструкторе в JSON-формате
    [ServiceFilter(typeof(ProfileAttribute))]
    [HttpGet("InstructorInfo/{id:int}")]
    public IActionResult InstructorInfo(int id) => 
        Json(_travelCompany.Instructors.First(b => b.Id == id));
}