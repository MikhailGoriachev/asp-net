using Microsoft.AspNetCore.Mvc;
using TagHelpersDemo.Models;

namespace TagHelpersDemo.Controllers;
public class HomeController : Controller
{
    // использование первого тэг-хелпера
    public IActionResult Index() => View();

    // использование асинхронного тег-хелпера
    public IActionResult DemoTagHelperAsynchronous() => View();

    // атрибуты тег-хелпера
    public IActionResult DemoTagHelpersAttributes() => View();


    // модель простых данных для отображения в тег-хелпере
    // коллекция строк, строку считаем простым типом данных
    public IActionResult DemoTagHelpersModels() {
        List<string> fruits = new List<string> { "груши", "яблоки", "вишни", "черешни", "сливы"};
        return View(fruits);
    } // DemoTagHelpersModels


    // сложные модели в тег-хелпере
    // ссылочный тип данных считаем сложным
    public IActionResult DemoTagHelpersComplexModels() {
        // Пример сложного типа данных
        Person person = new Person(DateTime.Now.Millisecond, "Романова Н.А.", 34);

        return View(person);
    } // DemoTagHelpersComplexModels
} // class HomeController
