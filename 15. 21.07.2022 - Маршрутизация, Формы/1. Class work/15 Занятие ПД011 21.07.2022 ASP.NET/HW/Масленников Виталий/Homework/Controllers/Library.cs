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
            _books = _source;
            Serialize(_books);
        }
    }
    
    // GET Library/Index
    public IActionResult Index()
    {
        return View(_books);
    }

    // GET Library/Addbook
    public IActionResult AddBook()
    {
        var newBook = _source.Random();
        newBook!.Id = _books.Any() ? _books.Max(b => b.Id + 1) : 1;
        newBook.Amount = Random.Shared.Next(1, 41);
        newBook.Price = Random.Shared.Next(4, 21) * 100;
        
        _books.Add(newBook);
        Serialize(_books);
        
        return View("Index", _books);
    }

    // GET Library/RemoveBook
    public IActionResult RemoveBook(int id)
    {
        _books.Remove(_books.FirstOrDefault(b => b.Id == id)!);
        Serialize(_books);
        return View("Index", _books);
    }

    // GET Library/OrderByAuthor
    public IActionResult OrderByAuthor()
    {
        ViewBag.Title = "Книги, упорядоченные по автору";
        return View("Index", _books.OrderBy(b => b.Author));
    }

    // GET Library/OrderByYear
    public IActionResult OrderByYear()
    {
        ViewBag.Title = "Книги, упорядоченные по году издания";
        return View("Index", _books.OrderBy(b => b.Year));
    }

    // GET Library/WhereMaxAmounts
    public IActionResult WhereMaxAmounts()
    {
        ViewBag.Title = "Книги, с максимальным количеством";
        return View("Index", _books.Where(b => b.Amount == _books.Max(book => book.Amount)));
    }

    // GET Library/Oldest
    public IActionResult Oldest()
    {
        ViewBag.Title = "Книги, с самым давним годом издания";
        return View("Index", _books.Where(b => b.Year == _books.Min(book => book.Year)));
    }

    // GET Library/Newest
    public IActionResult Newest()
    {
        ViewBag.Title = "Книги, с самым новым годом издания";
        return View("Index", _books.Where(b => b.Year == _books.Max(book => book.Year)));
    }

    
    private void Serialize(IEnumerable<Book> books) =>
        System.IO.File.WriteAllText(_path, JsonConvert.SerializeObject(books, Formatting.Indented));

    private List<Book> Deserialize() =>
        JsonConvert.DeserializeObject<List<Book>>(System.IO.File.ReadAllText(_path))!;
    
    // Исходные данные информации о книгах
    private List<Book> _source = new()
    {
        new Book(1, "451° по Фаренгейту", "Рэй Брэдбери", "cover1.jpg", 1953, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(2, "Изучаем Java", "К. Бейтс", "cover2.jpg", 2019, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(3, "Программирование на JavaScript", "Васильев А.Н.", "cover3.jpg", 2019, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(4, "1С: Предприятие. Практическое пособие разработчика", "Радченко Н.А.", "cover4.jpg", 2012, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(5  ,"Путь программиста", 			"Джон Сонмез", 			"cover5.jpg" , 2016, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(6  ,"Путь самурая (Хагакурэ)", 	"Ямамото Цунэтомо", 	"cover6.jpg" , 2019, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(7  ,"Совершенный код", 			"Стив Макконнелл", 		"cover7.jpg" , 1993, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(8  ,"Искусство программирования", 	"Эрвин Кнут", 			"cover8.jpg" , 1968, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(9  ,"Refactoring", 				"Кент Бек", 			"cover9.jpg" , 1999, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(10 ,"Философия Java", 				"Брюс Эккель", 			"cover10.jpg", 1998, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(11 ,"Собачье счастье", 			"Анна Фролова", 		"cover11.jpg", 2021, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(12 ,"JavaScript с нуля", 		   	"Чиннатхамби Кирупа", 	"cover12.jpg", 2021, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(13 ,"Design Patterns", 			"Эрих Гамма", 			"cover13.jpg", 1994, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(14 ,"Паттерны проектирования", 	"Эрик Фриман", 			"cover14.jpg", 2004, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
    };
}