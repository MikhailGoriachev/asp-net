using System.Text;
using ASP_NET_15_FormAspNet.Infrastructure;
using ASP_NET_15_FormAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASP_NET_15_FormAspNet.Controllers;

public class TaskController : Controller
{
    // путь к файлу
    private const string FilePath = "App_Data/Books.json";
    
    private List<Book> _books = new List<Book>();

    public TaskController() {
        if (!System.IO.File.Exists(FilePath)) {
            Generate();
            Serialize();
        }
        else {
            Deserialize();
        }
    }
    
    // GET
    public IActionResult Index() {
        ViewBag.Title = "Вывод исходной коллекции";
        return View("Library", _books);
    }

    // добавление книги
    public IActionResult AddBook() {
        ViewBag.Title = "Коллекция книг после добавления книги";
        int id = _books.Count > 0 ? _books.Max(item => item.Id) + 1 : 1;
        _books.Insert(0, Utilities.GetBook(id));
        Serialize();
        return View("Library", _books);
    }

    // удаление книги
    public IActionResult RemoveBook(int id) {
        ViewBag.Title = "Коллекция книг, после удаления книги";  
        _books.Remove(_books.FirstOrDefault(item=>item.Id == id)!);
        Serialize();
        return View("Library", _books);
    }
    
    // вывод книг упорядоченных по фамилиям авторов
    public IActionResult OrderByFio() {
        ViewBag.Title = "Коллекция книг упорядоченная по фамилиям авторов";
        _books = _books.OrderBy(item => item.Fio).ToList();
        return View("Library", _books);
    }
    
    // вывод книг упорядоченных по годам издания
    public IActionResult OrderByYear() {
        ViewBag.Title = "Коллекция книг упорядоченная по году издания";
        _books = _books.OrderBy(item => item.Year).ToList();
        return View("Library", _books);
    }
    
    // вывод книг упорядоченных по убыванию цены
    public IActionResult OrderByDescendingPrice() {
        ViewBag.Title = "Коллекция книг упорядоченная по году издания";
        _books = _books.OrderByDescending(item => item.Price).ToList();
        return View("Library", _books);
    }
     
    // вывод книг с максимальным количеством книг
    public IActionResult SelectByAmountMax() {
        ViewBag.Title = "Коллекция книг с максимальным колличеством книг";
        int maxAmount = _books.Max(item => item.Amount);
        _books = _books.Where(item => item.Amount == maxAmount).ToList();
        return View("Library", _books);
    }
   
    // вывод книг с минимальным количеством книг
    public IActionResult SelectByAmountMin() {
        ViewBag.Title = "Коллекция книг с минимальным колличеством книг";
        int minAmount = _books.Min(item => item.Amount);
        _books = _books.Where(item => item.Amount == minAmount).ToList();
        return View("Library", _books);
    }
    
    // вывод самых старых книг
    public IActionResult SelectByOldBook() {
        ViewBag.Title = "Коллекция самых старых книг";
        int minYear = _books.Min(item => item.Year);
        _books = _books.Where(item => item.Year == minYear).ToList();
        return View("Library", _books);
    }
    
    // вывод самых новых книг
    public IActionResult SelectByNewBook() {
        ViewBag.Title = "Коллекция самых новых книг";
        int maxYear = _books.Max(item => item.Year);
        _books = _books.Where(item => item.Year == maxYear).ToList();
        return View("Library", _books);
    }
    
    // вывод самых дорогих книг
    public IActionResult SelectByExpensiveBook() {
        ViewBag.Title = "Коллекция самых дорогих книг";
        int maxPrice = _books.Max(item => item.Price);
        _books = _books.Where(item => item.Price == maxPrice).ToList();
        return View("Library", _books);
    }
    
    // вывод самых дешевых книг
    public IActionResult SelectByCheapBook() {
        ViewBag.Title = "Коллекция самых дешевых книг";
        int minPrice = _books.Min(item => item.Price);
        _books = _books.Where(item => item.Price == minPrice).ToList();
        return View("Library", _books);
    }
    
    
    
    // ========================================================================     
    // генерация данных 
    private void Generate()  {
        if (_books.Count > 0) _books.Clear();
        for (int i = 0; i < 10; i++) {
            _books.Add(Utilities.GetBook(i + 1));
        } 
    } // Generate
     
    // Сериализация в JSON
    private void Serialize() {
        string json = JsonConvert.SerializeObject(_books, Formatting.Indented);
        System.IO.File.WriteAllText(FilePath, json, Encoding.UTF8);
    } // Serialize

    // Десериализация из JSON
    private void Deserialize() {
        string json = System.IO.File.ReadAllText(FilePath, Encoding.UTF8);
        _books = JsonConvert.DeserializeObject<List<Book>>(json);
    } // Deserialize
}