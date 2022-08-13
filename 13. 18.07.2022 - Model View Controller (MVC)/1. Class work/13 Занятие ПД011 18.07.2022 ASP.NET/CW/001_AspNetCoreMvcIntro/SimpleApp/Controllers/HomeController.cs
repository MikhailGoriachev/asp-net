using Microsoft.AspNetCore.Mvc;
using SimpleApp.Models;

namespace SimpleApp.Controllers;
public class HomeController : Controller
{

    // GET Home/Index
    public IActionResult Index() {

        // передать в представление данные через модель
        // (параметр вызова попапет в свойство Model представления) 
        int seconds = DateTime.Now.Second;
        return View(seconds);
    } // Index


    // GET Home/Number
    public IActionResult Number() {
        // передать в представление данные через ViewBag, ViewData
        int seconds = DateTime.Now.Second;
        ViewBag.Seconds = seconds;
        ViewData["Seconds"] = seconds;

        // через свойство Model ничего не передаем
        return View();
    } // Number

    

    // GET Home/Driver
    public IActionResult Driver() {
        // ссылочный тип,передаваемый представлению через модель 
        Driver driver = new Driver("Василий", 23);

        // передаем объект через свойство Model 
        return View(driver);
    } // Driver


    // Передача коллекции через строго типизированную модель
    // строгая типизация модели задается в представлении, не в методе действия
    // GET /Home/Products
    public IActionResult Products() {
        // коллекцию читаем из файла в папке App_Data
        var products = ReadFromFile();

        return View(products);
    } // Products

    // путь к файлу даных
    private readonly string _path = "App_Data/data.txt";

    // В контроллере могут быть приватные метды, они не являются 
    // методами действия, не участвуют в маршрутизации
    // чтение из файла данных, парсинг коллекции
    private List<Product> ReadFromFile() {
        // чтение из файла
        string[] lines = System.IO.File.ReadAllLines(_path);

        // парсинг строк в коллекцию
        List<Product> result = new List<Product>();
        foreach (string line in lines) {
            string[] items = line.Split(',');

            Product product = new Product {
                Id = int.Parse(items[0].Trim()),
                Name = items[1].Trim(),
                Price = double.Parse(items[2].Trim())
            };

            result.Add(product);
        } // foreach

        // вернуть коллекцию
        return result;
    } // ReadFromFile

    // Пример метода действия, вызывающего заданное по имени представление
    // GET /Home/Other
    public ActionResult Other() {
        // коллекцию читаем из файла в папке App_Data
        var products = ReadFromFile();

        // задать имя преставления, коллекцию данных
        return View("Products", products);
    } // Other
} // class HomeController

