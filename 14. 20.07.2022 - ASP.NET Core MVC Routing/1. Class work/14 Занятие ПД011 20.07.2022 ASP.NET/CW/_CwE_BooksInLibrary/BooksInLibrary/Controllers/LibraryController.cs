using BooksInLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksInLibrary.Controllers;
public class LibraryController : Controller
{
    private static List<Book> _books = new List<Book>(new[] {
        new Book( 1, "Бейтс К.", "Как программировать на Java", 2012, 5, "cover001.jpg"),
        new Book( 2, "Васильев А.Н.", "Как программировать на JavaScript", 2019, 2, "cover002.jpg"),
        new Book( 3, "Радченко М.С.", "1С:Предприятие 8.3. Разработка приложений", 2013, 5, "cover003.jpg"),
        new Book( 4, "Гальярди В.", "Раздельный Django", 2022, 6, "cover004.jpg"),
        new Book( 5, "Троелсен Э.", "Язык C# 9. Платформа .NET 5", 2021, 8, "cover005.jpg"),
        new Book( 6, "Боон К.", "Pascal для всех", 2002, 2, "cover006.jpg"),
        new Book( 7, "Фаронов В.В.", "Delphi 3", 2003, 8, "cover007.jpg"),
        new Book( 8, "Кузнецов В.М.", "PHP 7. Самоучитель", 2019, 1, "cover008.jpg"),
        new Book( 9, "Постолит А.", "Основы искусственного интеллекта в примерах на Python", 2021, 5, "cover009.jpg"),
        new Book(10, "МакФарланд Д.", "JavaScript и jQuery", 2021, 9, "cover010.jpg"),
        new Book(11, "Блинов Н.Н.", "Java. Методы программирования", 2013, 6, "cover011.jpg"),
    });
    

    // Вывод списка книг
    // GET /Home/Index
    public IActionResult Index() => View(_books);

    // Добавление книги
    // GET /Home/
    public IActionResult AddBook() {
        Book book = new Book(
            _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1,
            "Файн Я.В.",
            "Angular и TypeScript. Сайтостроение для профессионалов",
            2020,
            3,
            "cover012.jpg"
        );
        _books.Add(book);

        // показать коллекцию вместе с добавленной книгой
        return View("Index", _books);
    } // AddBook


    // Книги по автору
    public IActionResult OrderByAuthor() {
        return View("Index", _books.OrderBy(b => b.Author));
    } // OrderByAuthor


    // Книги по году издания
    public IActionResult OrderByPubYear() {
        return View("Index", _books.OrderBy(b => b.PubYear));
    } // OrderByPubYear

    // Удаление книг по Id
    public IActionResult DeleteById(int id) {
        // удаление книги из коллекции
        _books.Remove(_books.First(b => b.Id == id));
        return View("Index", _books);
    } // DeleteById
} // class LibraryController

