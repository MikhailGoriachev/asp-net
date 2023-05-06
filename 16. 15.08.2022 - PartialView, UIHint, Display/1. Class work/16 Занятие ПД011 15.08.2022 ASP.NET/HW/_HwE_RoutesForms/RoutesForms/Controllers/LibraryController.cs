using System.Text;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using RoutesForms.Infrastructure;
using RoutesForms.Models.Task01;

namespace RoutesForms.Controllers;

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
    // GET /Library/Index
    public IActionResult Index() {
        ViewBag.Header = "Книги, хранящиеся в библиотеке";

        return View(_books);
    } // Index


    // Добавление книги - вывод страницы с формой добавления
    // GET /Library/Add
    public IActionResult AddBook() {
        Book book = new Book() {Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1 };

        // переход на страницу корректировки данных книги
        return View("UpdateById", book);
    } // AddBook


    // Удаление книг по Id
    public IActionResult DeleteById(int id) {
        // удаление книги из коллекции
        _books.Remove(_books.First(b => b.Id == id));
        Serialize();

        return View("Index", _books);
    } // DeleteById


    // Вывод страницы с формой редактирования книги
    public IActionResult UpdateById(int id) {
        Book book = _books.First(b => b.Id == id);

        return View(book);
    } // UpdateById

    // обработчик сведений, полученных из формы редактирования книги
    [HttpPost]
    public IActionResult Update(Book bookData) {
        int index = _books.FindIndex(b => b.Id == bookData.Id);

        // если индекса нет в коллекции, это добавляемая книга 
        if (index < 0) {
            _books.Insert(0, bookData);
        } else { 
            _books[index] = bookData;
        } // if
        Serialize();

        return View("Index", _books);
    } // Update


    // выдача сведений о книгах, сведения упорядочены по заданию
    public IActionResult OrderBy(string key) {
        // отсортированная коллекция сведенйи о книгах
        IEnumerable<Book> result = null!;

        switch (key) {
            // Выдача сведений о всех книгах, упорядоченных по фамилиям авторов
            case "author":
                ViewBag.Header = "Книги, упорядоченные по фамилиям авторов";
                result = _books.OrderBy(b => b.Author);
                break;

            // Выдача сведений о всех книгах, упорядоченных по годам издания
            case "pub-year":
                ViewBag.Header = "Книги, упорядоченные по годам издания";
                result = _books.OrderBy(b => b.PubYear);
                break;

            // Выдача сведений о всех книгах, упорядоченных по убыванию цены
            case "cost-descend":
                ViewBag.Header = "Книги, упорядоченные по убыванию цены";
                result = _books.OrderByDescending(b => b.Cost);
                break;
        } // switch

        // отобразить отсортированную копию коллекции
        return View("Index", result);
    } // OrderBy


    // Выборка данных по книгам в библиотеке
    public IActionResult Where(string key) {
        // выборка из коллекции
        IEnumerable<Book> result = null!;

        switch (key) {
            // Выдача сведений о книгах с количеством, равным максимальному
            case "max-quantity":
                ViewBag.Header = "Книги с количеством, равным максимальному";
                int maxQuantity = _books.Max(b => b.Quantity);
                result = _books.Where(b => b.Quantity == maxQuantity);
                break;

            // Выдача сведений о самых старых книгах
            case "oldest":
                ViewBag.Header = "Самые старые книги библиотеки";
                int minPubYear = _books.Min(b => b.PubYear);
                result = _books.Where(b => b.PubYear == minPubYear);
                break;

            // Выдача сведений о самых новых книгах
            case "newest":
                ViewBag.Header = "Самые новые книги библиотеки";
                int maxPubYear = _books.Max(b => b.PubYear);
                result = _books.Where(b => b.PubYear == maxPubYear);
                break;

            // Выдача сведений о самых дорогих книгах
            case "most-expensive":
                ViewBag.Header = "Самые дорогие книги библиотеки";
                int maxCost = _books.Max(b => b.Cost);
                result = _books.Where(b => b.Cost == maxCost);
                break;


            // Выдача сведений о книгах с минимальной ценой
            case "most-unexpensive":
                ViewBag.Header = "Книги библиотеки с минимальной ценой";
                int minCost = _books.Min(b => b.Cost);
                result = _books.Where(b => b.Cost == minCost);
                break;
        } // switch

        // отобразить выборку из коллекции
        return View("Index", result);
    } // WhereMaxQuantity


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

