using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace HomeWork.Controllers;

public class LibraryController : Controller
{
    // список книг
    public List<Book>? Books { get; set; }

    // имя файла для сохранения
    public string FileName { get; set; } = "books.json";

    #region Конструкторы

    // конструктор по умолчанию
    public LibraryController()
    {
        if (!LoadData())
        {
            Books = new()
            {
                new Book("Джон Сонмез", "Путь программиста", 2016, Utils.GetInt(5, 10), Utils.GetInt(10, 15) * 100,
                    "book-path-programmer.jpg"),
                new Book("Ямамото Цунэтомо", "Путь самурая (Хагакурэ)", 2019, Utils.GetInt(5, 10),
                    Utils.GetInt(10, 15) * 100, "book-samurai.jpg"),
                new Book("Стив Макконнелл", "Совершенный код", 1993, Utils.GetInt(5, 10), Utils.GetInt(10, 15) * 100,
                    "book-1.jpg"),
                new Book("Эрвин Кнут", "Искусство программирования", 1968, Utils.GetInt(5, 10),
                    Utils.GetInt(10, 15) * 100, "book-2.jpg"),
                new Book("Кент Бек", "Refactoring", 1999, Utils.GetInt(5, 10), Utils.GetInt(10, 15) * 100,
                    "book-3.jpg"),
                new Book("Брюс Эккель", "Философия Java", 1998, Utils.GetInt(5, 10), Utils.GetInt(10, 15) * 100,
                    "book-4.jpg"),
                new Book("Анна Фролова", "Собачье счастье", 2021, Utils.GetInt(5, 10), Utils.GetInt(10, 15) * 100,
                    "book-5.jpg"),
                new Book("Чиннатхамби Кирупа", "JavaScript с нуля", 2021, Utils.GetInt(5, 10),
                    Utils.GetInt(10, 15) * 100, "book-6.jpg"),
                new Book("Эрих Гамма", "Design Patterns", 1994, Utils.GetInt(5, 10), Utils.GetInt(10, 15) * 100,
                    "book-7.jpg"),
                new Book("Эрик Фриман", "Паттерны проектирования", 2004, Utils.GetInt(5, 10),
                    Utils.GetInt(10, 15) * 100, "book-8.jpg"),
            };

            SaveData();
        }
    }

    #endregion

    #region Методы

    #region Представления

    // вывод всех книг
    public IActionResult Index()
    {
        ViewBag.Title = "Книги. Исходные данные";

        return View(Books);
    }


    // добавление книги
    public IActionResult AddBook()
    {
        Books!.Insert(0, new Book("Стив Макконнелл", "Совершенный код", 1993, Utils.GetInt(5, 10), Utils.GetInt(10, 15) * 100,"book-path-programmer.jpg"));

        ViewBag.Title = $"Книги. Добавлена книга \"{Books[0].Title}\"";

        SaveData();

        return View("Index", Books);
    }


    // удаление книги
    public IActionResult RemoveBook(int id)
    {
        ViewBag.Title = $"Книга. Удалена id - {id}";

        Books!.Remove(Books.FirstOrDefault(b => b.Id == id)!);

        SaveData();

        return View("Index", Books);
    }


    // упорядочивание по фамилиям авторов
    public IActionResult OrderByAuthor()
    {
        ViewBag.Title = $"Книга. Упорядочивание по фамилиям авторов";

        return View("Index", Books!.OrderBy(b => b.Author).ToList());
    }


    // упорядочивание по годам издания
    public IActionResult OrderByPubYear()
    {
        ViewBag.Title = $"Книга. Упорядочивание по годам издания";

        return View("Index", Books!.OrderBy(b => b.PubYear).ToList());
    }

    // максимальное количество
    public IActionResult SelectByMaxCount()
    {
        int maxCount = Books!.Max(b => b.Count);

        ViewBag.Title = $"Книга. Максимальное количество: {maxCount} шт.";

        return View("Index", Books!.Where(b => b.Count == maxCount).ToList());
    }


    // самые старые книги
    public IActionResult SelectByOlder()
    {
        int minYear = Books!.Min(b => b.PubYear);

        ViewBag.Title = $"Книга. Cамые старые книги. Минимальный год выпуска: {minYear}";

        return View("Index", Books!.Where(b => b.PubYear == minYear).ToList());
    }


    // самые новые книги
    public IActionResult SelectByNewest()
    {
        int maxYear = Books!.Max(b => b.PubYear);

        ViewBag.Title = $"Книга. Cамые новые книги. Максимальный год выпуска: {maxYear}";

        return View("Index", Books!.Where(b => b.PubYear == maxYear).ToList());
    }

    #endregion

    #region Обработка

    // сохранение коллекции в JSON
    public void SaveData() => System.IO.File.WriteAllText(Path.Combine("App_Data", FileName), JsonConvert.SerializeObject(Books));


    // загрузка коллекции из JSON
    public bool LoadData()
    {
        string path = Path.Combine("App_Data", FileName);

        bool exist = System.IO.File.Exists(path);

        Books = exist ? JsonConvert.DeserializeObject<List<Book>>(System.IO.File.ReadAllText(path)) : new List<Book>();

        return exist;
    }

    #endregion

    #endregion
}
