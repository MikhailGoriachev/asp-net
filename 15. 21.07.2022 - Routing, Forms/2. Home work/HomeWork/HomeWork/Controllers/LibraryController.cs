using System.Collections;
using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace HomeWork.Controllers;

public class LibraryController : Controller
{
    // список книг
    public IEnumerable<Book>? Books { get; set; }

    // имя файла для сохранения
    public string FileName { get; set; } = "books.json";

    // коллекция сортировок
    public Dictionary<string, (string Title, Func<IEnumerable<Book>> Function)> OrderFuncs { get; set; }

    // коллекция выборок
    public Dictionary<string, (string Title, Func<IEnumerable<Book>> Function)> SelectionFuncs { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public LibraryController()
    {
        if (!LoadData())
        {
            Books = new Book[]
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

        // сортировки
        OrderFuncs = new()
        {
            // исходная последовательность
            ["default"] = ("Исходная последовательность", () => Books!),

            // упорядочивание по авторам
            ["author"] = ("Упорядочивание по авторам", () => Books!.OrderBy(b => b.Author)),

            // упорядочивание по годам издания
            ["pubYear"] = ("Упорядочивание по годам издания", () => Books!.OrderBy(b => b.PubYear)),

            // упорядочивание по убыванию цены
            ["descPrice"] = ("Упорядочивание по убыванию цены", () => Books!.OrderByDescending(b => b.Price)),
        };

        // выборки
        SelectionFuncs = new()
        {
            // минимальное количество
            ["minCount"] = ("Минимальное количество", () =>
            {
                int minCount = Books!.Min(b => b.Count);
                return Books!.Where(b => b.Count == minCount);
            }),

            // максимальное количество
            ["maxCount"] = ("Максимальное количество", () =>
            {
                int maxCount = Books!.Max(b => b.Count);
                return Books!.Where(b => b.Count == maxCount);
            }),

            // самые старые
            ["older"] = ("Самые старые", () =>
            {
                int minYear = Books!.Min(b => b.PubYear);
                return Books!.Where(b => b.PubYear == minYear);
            }),

            // самые новые
            ["newest"] = ("Самые новые", () =>
            {
                int maxYear = Books!.Max(b => b.PubYear);
                return Books!.Where(b => b.PubYear == maxYear);
            }),

            // самые дорогие
            ["maxPrice"] = ("Самые дорогие", () =>
            {
                int maxPrice = Books!.Max(b => b.Price);
                return Books!.Where(b => b.Price == maxPrice);
            }),

            // минимальная цена
            ["minPrice"] = ("Самые дешевые", () =>
            {
                int minPrice = Books!.Min(b => b.Price);
                return Books!.Where(b => b.Price == minPrice);
            }),
        };
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
        ViewBag.IsAdd = true;
        return View("BookForm", new BookBindingForm(0, "", "", DateTime.Now.Year, 0, 0, default));
    }

    [HttpPost]
    public IActionResult AddBook(BookBindingForm book)
    {
        Books = Books!.Prepend(new Book(book.Author, book.Title, book.PubYear, book.Count, book.Price,
            book.FormFile?.FileName ?? "default-book.jpg"));

        ViewBag.Title = $"Книги. Добавлена книга \"{book.Title}\"";

        // загрузка файла с обложкой
        if (book.FormFile != null)
            Utils.SaveFile(@"wwwroot\images\books", book.FormFile);
        SaveData();

        return View("Index", Books);
    }

    // редактирование книги
    public IActionResult EditBook(int id)
    {
        ViewBag.IsAdd = false;

        Book? book = Books!.FirstOrDefault(b => b.Id == id);

        return book == null
            ? NotFound()
            : View("BookForm",
                new BookBindingForm(book.Id, book.Author, book.Title, book.PubYear, book.Count, book.Price, default));
    }

    [HttpPost]
    public IActionResult EditBook(BookBindingForm book)
    {
        Book editBook = Books?.FirstOrDefault(b => b.Id == book.Id)!;

        // установка значпний в поля редактируемой книги
        editBook.Author = book.Author;
        editBook.Title = book.Title;
        editBook.PubYear = book.PubYear;
        editBook.Count = book.Count;
        editBook.Price = book.Price;
        editBook.FileName = book.FormFile?.FileName ?? editBook.FileName;

        // загрузка файла с обложкой
        if (book.FormFile != null)
            Utils.SaveFile(@"wwwroot\images\books", book.FormFile);

        SaveData();

        return View("Index", Books);
    }

    // удаление книги
    public IActionResult RemoveBook(int id)
    {
        ViewBag.Title = $"Книги. Исходные данные";

        // Books!.ToList().Remove(Books!.FirstOrDefault(b => b.Id == id)!);

        Books = Books!.Where(b => b.Id != id);

        SaveData();

        return View("Index", Books);
    }


    // упорядочивание по названию сортировки
    public IActionResult OrderBy(string orderName)
    {
        var order = OrderFuncs.ContainsKey(orderName) ? OrderFuncs[orderName] : OrderFuncs["default"];

        ViewBag.Title = order.Title;

        return View("Index", order.Function());
    }


    // выборка по названию выборки
    public IActionResult SelectionBy(string selectionName)
    {
        var selection = SelectionFuncs.ContainsKey(selectionName) ? SelectionFuncs[selectionName] : OrderFuncs["default"];

        ViewBag.Title = selection.Title;

        return View("Index", selection.Function());
    }

    #endregion

    #region Обработка

    // сохранение коллекции в JSON
    public void SaveData() =>
        System.IO.File.WriteAllText(Path.Combine("App_Data", FileName), JsonConvert.SerializeObject(Books));


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

// модель для привязки к полям формы
public record BookBindingForm(int Id, string Author, string Title, int PubYear, int Count, int Price,
    IFormFile? FormFile);
