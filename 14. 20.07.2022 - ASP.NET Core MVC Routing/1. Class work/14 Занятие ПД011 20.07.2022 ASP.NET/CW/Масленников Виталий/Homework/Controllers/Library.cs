using Homework.Models;
using Homework.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Homework.Controllers;

public class Library : Controller
{
    // Путь к файлу данных
    private readonly string _path = "App_Data/books.json";

    
    private List<Book> _books;

    public Library()
    {
        if (System.IO.File.Exists(_path))
            _books = Deserialize();
        else
        {
            _books = new List<Book>
            {
                new(1, "451° по Фаренгейту", "Рэй Брэдбери", "cover1.jpg", 1953, 12),
                new(2, "Изучаем Java", "К. Бейтс", "cover2.jpg", 2019, 6),
                new(3, "Программирование на JavaScript", "Васильев А.Н.", "cover3.jpg", 2019, 8),
                new(4, "1С: Предприятие. Практическое пособие разработчика", "Радченко Н.А.", "cover4.jpg", 2012,
                    8),
            };
            Serialize(_books);
        }
    }
    
    // GET
    public IActionResult Index()
    {
        return View(_books);
    }

    public IActionResult Addbook()
    {
        var newBook = _source.Random();
        newBook!.Id = _books.Any() ? _books.Max(b => b.Id + 1) : 1;
        newBook!.Amount = Random.Shared.Next(1, 21);
        _books.Add(newBook);
        Serialize(_books);
        return View("Index", _books);
    }

    public IActionResult RemoveBook(int id)
    {
        _books.Remove(_books.FirstOrDefault(b => b.Id == id)!);
        Serialize(_books);
        return View("Index", _books);
    }

    public IActionResult OrderByAuthor()
    {
        return View("Index", _books.OrderBy(b => b.Author));
    }

    public IActionResult OrderByYear()
    {
        return View("Index", _books.OrderBy(b => b.Year));
    }
    
    
    private void Serialize(IEnumerable<Book> books) =>
        System.IO.File.WriteAllText(_path, JsonConvert.SerializeObject(books, Formatting.Indented));

    private List<Book> Deserialize() =>
        JsonConvert.DeserializeObject<List<Book>>(System.IO.File.ReadAllText(_path))!;
    
    private List<Book> _source = new()
    {
        new Book(1, "451° по Фаренгейту", "Рэй Брэдбери", "cover1.jpg", 1953, 12),
        new Book(2, "Изучаем Java", "К. Бейтс", "cover2.jpg", 2019, 6),
        new Book(3, "Программирование на JavaScript", "Васильев А.Н.", "cover3.jpg", 2019, 8),
        new Book(4, "1С: Предприятие. Практическое пособие разработчика", "Радченко Н.А.", "cover4.jpg", 2012,
            8),
    };
}