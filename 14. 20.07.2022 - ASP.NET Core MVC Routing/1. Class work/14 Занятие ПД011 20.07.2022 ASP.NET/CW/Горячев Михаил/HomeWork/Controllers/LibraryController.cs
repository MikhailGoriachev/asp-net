using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers;

public class LibraryController : Controller
{
    // список книг
    public static List<Book> Books { get; set; }

    #region Конструкторы

    // конструктор статический
    static LibraryController()
    {
        Books = new()
        {
            new Book("Джон Сонмез", "Путь программиста", 2016, Utils.GetInt(0, 20), "book-path-programmer.jpg"),
            new Book("Ямамото Цунэтомо", "Путь самурая (Хагакурэ)", 2019, Utils.GetInt(0, 20), "book-samurai.jpg"),
            new Book("Стив Макконнелл", "Совершенный код", 1993, Utils.GetInt(0, 20), "book-1.jpg"),
            new Book("Эрвин Кнут", "Искусство программирования", 1968, Utils.GetInt(0, 20), "book-2.jpg"),
            new Book("Кент Бек", "Refactoring", 1999, Utils.GetInt(0, 20), "book-3.jpg"),
            new Book("Брюс Эккель", "Философия Java", 1998, Utils.GetInt(0, 20), "book-4.jpg"),
            new Book("Анна Фролова", "Собачье счастье", 2021, Utils.GetInt(0, 20), "book-5.jpg"),
            new Book("Чиннатхамби Кирупа", "JavaScript с нуля", 2021, Utils.GetInt(0, 20), "book-6.jpg"),
            new Book("Эрих Гамма", "Design Patterns", 1994, Utils.GetInt(0, 20), "book-7.jpg"),
            new Book("Эрик Фриман", "Паттерны проектирования", 2004, Utils.GetInt(0, 20), "book-8.jpg"),
        };
    }
    #endregion

    #region Методы

    // вывод всех книг
    public IActionResult Index()
    {
        ViewBag.Title = "Книги. Исходные данные";

        return View(Books);
    }


    // добавление книги
    public IActionResult AddBook()
    {
        Books.Insert(0, new Book("Стив Макконнелл", "Совершенный код", 1993, Utils.GetInt(0, 20), "book-path-programmer.jpg"));

        ViewBag.Title = $"Книги. Добавлена книга \"{Books[0].Title}\"";

        return View("Index", Books);
    }


    // удаление книги
    public IActionResult RemoveBook(int id)
    {
        ViewBag.Title = $"Книга. Удалена id - {id}";

        Books.Remove(Books.FirstOrDefault(b => b.Id == id)!);

        return View("Index", Books);
    }


    // упорядочивание по фамилиям авторов
    public IActionResult OrderByAuthor()
    {
        ViewBag.Title = $"Книга. Упорядочивание по фамилиям авторов";

        return View("Index", Books.OrderBy(b => b.Author).ToList());
    }


    // упорядочивание по годам издания
    public IActionResult OrderByPubYear()
    {
        ViewBag.Title = $"Книга. Упорядочивание по годам издания";

        return View("Index", Books.OrderBy(b => b.PubYear).ToList());
    }

    #endregion
}
