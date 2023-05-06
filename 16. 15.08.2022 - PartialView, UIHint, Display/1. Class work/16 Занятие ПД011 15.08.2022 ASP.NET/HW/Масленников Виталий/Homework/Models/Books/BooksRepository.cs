using Newtonsoft.Json;

namespace Homework.Models.Books;

public class BooksRepository : IBooksRepository
{
    private readonly string _dataFile = "App_Data/books.json";
    
    private readonly List<Book> _books;
    
    public BooksRepository()
    {
        if (File.Exists(_dataFile))
            _books = Deserialize();
        else
        {
            _books = GetSeedData();
            Serialize(_books);
        }
    }

    public IEnumerable<Book> Books => _books;
    
    public void AddBook(Book book)
    {
        book.Id = _books.Any() ? _books.Max(b => b.Id + 1) : 1;
        _books.Insert(0, book);
    }

    public void UpdateBook(Book book)
    {
        var find = _books.FirstOrDefault(b => b.Id == book.Id);
        if (find is null) return;
        find.Amount = book.Amount;
        find.Author = book.Author;
        find.Image = book.Image;
        find.Price = book.Price;
        find.Title = book.Title;
        find.Year = book.Year;
    }

    public void DeleteBook(int id)
    {
        var delete = _books.FirstOrDefault(b => b.Id == id);
        
        if(delete is not null)
            _books.Remove(delete);
    }

    public void SaveData() => Serialize(_books);
    
    private void Serialize(IEnumerable<Book> books) =>
        System.IO.File.WriteAllText(_dataFile, JsonConvert.SerializeObject(books, Formatting.Indented));

    private List<Book> Deserialize() =>
        JsonConvert.DeserializeObject<List<Book>>(System.IO.File.ReadAllText(_dataFile))!;
    
    private static List<Book> GetSeedData() =>new()
    {
        new Book(1, "451° по Фаренгейту", "Брэдбери Рэй", "cover1.jpg", 1953, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(2, "Изучаем Java", "Бейтс Берт", "cover2.jpg", 2019, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(3, "Программирование на JavaScript", "Васильев А.Н.", "cover3.jpg", 2019, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(4, "1С: Предприятие. Практическое пособие разработчика", "Радченко Н.А.", "cover4.jpg", 2012, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(5  ,"Путь программиста", 			"Сонмез Джон", 			"cover5.jpg" , 2016, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(6  ,"Путь самурая (Хагакурэ)", 	"Цунэтомо Ямамото", 	"cover6.jpg" , 2019, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(7  ,"Совершенный код", 			"Макконнелл Стив", 		"cover7.jpg" , 1993, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(8  ,"Искусство программирования", 	"Кнут Эрвин", 			"cover8.jpg" , 1968, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(9  ,"Refactoring", 				"Бек Кент", 			"cover9.jpg" , 1999, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(10 ,"Философия Java", 				"Эккель Брюс", 			"cover10.jpg", 1998, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(11 ,"Собачье счастье", 			"Фролова Анна", 		"cover11.jpg", 2021, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(12 ,"JavaScript с нуля", 		   	"Чиннатхамби Кирупа", 	"cover12.jpg", 2021, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(13 ,"Design Patterns", 			"Гамма Эрих", 			"cover13.jpg", 1994, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
        new Book(14 ,"Паттерны проектирования", 	"Фриман Эрик", 			"cover14.jpg", 2004, Random.Shared.Next(1,41), Random.Shared.Next(4,20) * 100),
    };
}