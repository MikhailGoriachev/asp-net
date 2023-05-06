using Microsoft.AspNetCore.Mvc;

namespace ActionResultsSamples.Controllers;

// материал на 18.08.2022
public class PartialViewController : Controller
{
    public IActionResult Index() {
        return View();
    }

    // запуск частичного представления без параметров
    public IActionResult PartialWithoutParams() {
        return PartialView();
    }

    // запуск частичного представления с параметрами
    public IActionResult PartialWithParams() {
        var fruits = new List<string> { "Яблоко", "Груша", "Вишня", "Черешня", "Слива", "Абрикоса" };
        return PartialView(fruits);
    }  
    
    // запуск частичного представления с заданием имени файла, без параметров
    public IActionResult NamedPartialWithoutParams() {
        return PartialView("NamedWithoutParams");
    }

    // запуск частичного представления с заданием имени файла, с параметрами
    public IActionResult NamedPartialWithParams() {
        var vegetables = new List<string> { "Помидор", "Морковь", "Картофель", "Лук", "Огурец", "Редис" };
        return PartialView("NamedWithParams", vegetables);
    }
} // class PartialViewController

