using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class PeopleController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Header = "Коллекция персонажей.";

        return View();
    }


    // Вывод страницы с информацией о персонаже
    public IActionResult PeopleInfoAll(int id)
    {
        ViewBag.Header = "Полная информация о персонаже.";

        return View(id);
    } // PeopleInfoAll


    public IActionResult PeopleSelectMass()
    {
        ViewBag.Header = "Персонажи с заданным диапазоном веса.";

        return View();
    } // PeopleSelectMass


    public IActionResult PeopleMass()
    {
        ViewBag.Header = "Персонажи упорядочены по возрастанию массы.";

        return View();
    } // PeopleMass


} // class PeopleController
