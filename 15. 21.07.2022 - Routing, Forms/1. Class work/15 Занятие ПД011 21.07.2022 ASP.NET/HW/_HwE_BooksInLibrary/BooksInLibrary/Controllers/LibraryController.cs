using System.Text;
using BooksInLibrary.Infrastructure;
using BooksInLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BooksInLibrary.Controllers;
public class LibraryController : Controller
{
    // имя файла данных
    public string FilePath = "App_Data/Books.json";

    // коллекция книг 
    private List<Book> _books = null!;

    // конструктор вызывается на каждом запросе к контроллеру
    public LibraryController() {
        // если файла данных нет - создать коллекцию и сохранить
        // ее в файле, если файл есть - загрузить коллекцию из файла
        if (!System.IO.File.Exists(FilePath)) {
            _books = Utils.InitializeBooks();
            Serialize();
        } else {
            Deserialize();
        } // if
    } // LibraryController


    // Вывод списка книг
    // GET /Home/Index
    public IActionResult Index() {
        ViewBag.Header = "Книги, хранящиеся в библиотеке";

        return View(_books);
    } // Index

    // Добавление книги
    // GET /Home/
    public IActionResult AddBook() {
        Book book = new Book(
            _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1,
            "Файн Я.В.",
            "Angular и TypeScript. Сайтостроение для профессионалов",
            2020,
            3,
            890,
            "cover012.jpg"
        );

        // добавление в начало коллекции, чтобы видеть добавленную 
        // книгу при рендеринне страницы
        _books.Insert(0, book);
        Serialize();

        // показать коллекцию вместе с добавленной книгой
        return View("Index", _books);
    } // AddBook

    
    // Удаление книг по Id
    public IActionResult DeleteById(int id) {
        // удаление книги из коллекции
        _books.Remove(_books.First(b => b.Id == id));
        Serialize();

        return View("Index", _books);
    } // DeleteById


    // Выдача сведений о всех книгах, упорядоченных по фамилиям авторов
    public IActionResult OrderByAuthor() {
        ViewBag.Header = "Книги, упорядоченные по фамилиям авторов";

        return View("Index", _books.OrderBy(b => b.Author));
    } // OrderByAuthor


    // Выдача сведений о всех книгах, упорядоченных по годам издания
    public IActionResult OrderByPubYear() {
        ViewBag.Header = "Книги, упорядоченные по годам издания";

        return View("Index", _books.OrderBy(b => b.PubYear));
    } // OrderByPubYear


    // Выдача сведений о книгах с количеством, равным максимальному
    public IActionResult WhereMaxQuantity() {
        ViewBag.Header = "Книги с количеством, равным максимальному";

        int maxQuantity = _books.Max(b => b.Quantity);
        return View("Index", _books.Where(b => b.Quantity == maxQuantity));
    } // WhereMaxQuantity


    // Выдача сведений о самых старых книгах
    public IActionResult WhereOldest() {
        ViewBag.Header = "Самые старые книги библиотеки";

        int minPubYear = _books.Min(b => b.PubYear);
        return View("Index", _books.Where(b => b.PubYear == minPubYear));
    } // WhereOldest


    // Выдача сведений о самых новых книгах
    public IActionResult WhereNewest(){
        ViewBag.Header = "Самые новые книги библиотеки";

        int maxPubYear = _books.Max(b => b.PubYear);
        return View("Index", _books.Where(b => b.PubYear == maxPubYear));
    } // WhereNewest


    // ========================================================================

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

} // class LibraryController

