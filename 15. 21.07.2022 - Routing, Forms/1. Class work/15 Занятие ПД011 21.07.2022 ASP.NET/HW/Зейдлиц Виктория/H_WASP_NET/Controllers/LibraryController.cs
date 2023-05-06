using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace H_WASP_NET.Controllers
{
    public class LibraryController : Controller
    {
        // список книг
        public static List<Book>? Books { get; set; }

        // путь к файлу данных
        private readonly string _path = "App_Data/books.json";

        // конструктор статический
        public LibraryController()
        {
            if (System.IO.File.Exists(_path))
                Books = Deserialize();
            else
            {
                Books = new()
                {
                    new Book(1,"Джон Сонмез", "Путь программиста", 2016, 4, 2050, "cover001.jpg"),
                    new Book(2,"Ямамото Цунэтомо", "Путь самурая (Хагакурэ)", 2019, 5, 800, "cover002.jpg"),
                    new Book(3,"Стив Макконнелл", "Совершенный код", 1993,2, 4000, "cover003.jpg"),
                    new Book(4,"Эрвин Кнут", "Искусство программирования", 1968,3, 3050, "cover004.jpg"),
                    new Book(5,"Кент Бек", "Refactoring", 1996, 2, 980, "cover006.jpg"),
                    new Book(6,"Брюс Эккель", "Философия Java", 1998,2, 3600, "cover007.jpg"),
                    new Book(7,"Анна Фролова", "Собачье счастье", 2021, 8, 1000, "cover008.jpg"),
                    new Book(8,"Чиннатхамби Кирупа", "JavaScript с нуля", 2021, 8, 670,  "cover005.jpg"),
                    new Book(9,"Эрих Гамма", "Design Patterns", 1994, 4, 2200, "cover012.jpg"),
                    new Book(10,"Эрик Фриман", "Паттерны проектирования", 2004, 3, 1800, "cover011.jpg")
                };
                Serialize(Books);
            }
        }


        public IActionResult Index()
        {
            ViewBag.Title = "Книги в библиотеке.";

            return View(Books);
        }

        // Добавление книги в коллекцию 
        public IActionResult Addbook()
        {
            ViewBag.Title = "Книга добавлена в библиотеку.";

            Book book = new Book(
            Books.Count > 0 ? Books.Max(b => b.Id) + 1 : 1,
            "Файн Я.В.", "Сайтостроение для профессионалов", 2020, 1, 2700, "cover009.jpg");

            Books.Add(book);
            Serialize(Books);
            return View("Index", Books);
        }


        // Удаление данных о книге из коллекции 
        public IActionResult RemoveBook(int id)
        {
            ViewBag.Title = "Книга удалена из библиотеки.";

            Books.Remove(Books.FirstOrDefault(b => b.Id == id)!);
            Serialize(Books);
            return View("Index", Books);
        }


        // Выдача сведений о всех книгах, упорядоченных по фамилиям авторов
        public IActionResult OrderByAuthor()
        {
            ViewBag.Title = "Книги упорядочены по автору.";

            return View("Index", Books.OrderBy(b => b.Autor));
        }


        // Выдача сведений о всех книгах, упорядоченных по годам издания
        public IActionResult OrderByYear()
        {
            ViewBag.Title = "Книги упорядочены по году издания.";
            return View("Index", Books.OrderBy(b => b.PubYear));
        }

        // Выдача сведений о книгах с количеством, равным максимальному
        public IActionResult QuantityMax()
        {
            ViewBag.Title = "Книги с количеством, равным максимальному.";
            var maxQuantity = Books.Max(b => b.Quantity);


            return View("Index", Books.Where(b => b.Quantity == maxQuantity));
        }


        // Выдача сведений о самых старых книгах
        public IActionResult OldBooks()
        {
            ViewBag.Title = "Самые старые книги.";
            var minYear = Books.Min(b => b.PubYear);


            return View("Index", Books.Where(b => b.PubYear == minYear));
        }

        // Выдача сведений о самых новых книгах
        public IActionResult NewBooks()
        {
            ViewBag.Title = "Самые новые книги.";
            var maxYear = Books.Max(b => b.PubYear);


            return View("Index", Books.Where(b => b.PubYear == maxYear));
        }

        private void Serialize(IEnumerable<Book> books) =>
           System.IO.File.WriteAllText(_path, JsonConvert.SerializeObject(books, Formatting.Indented));

        private List<Book> Deserialize() =>
            JsonConvert.DeserializeObject<List<Book>>(System.IO.File.ReadAllText(_path))!;

    } // class LibraryController
}
