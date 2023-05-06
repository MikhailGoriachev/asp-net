namespace HomeWork.Models;

// Класс Книга
public class Book
{
    // текущий максимальный id
    private static int _currentMaxId = 0;

    // id
    private int _id;

    public int Id
    {
        get => _id;
        set
        {
            _currentMaxId = value > _currentMaxId ? value : _currentMaxId;
            _id = value;
        }
    }

    // автор
    public string Author { get; set; } = null!;

    // название
    public string Title { get; set; } = null!;

    // год выпуска
    public int PubYear { get; set; }

    // количество
    public int Count { get; set; }

    // цена
    public int Price { get; set; }

    // файл с обложной
    public string FileName { get; set; } = null!;

    #region Конструкторы

    // конструктор по умолчанию
    public Book()
    {
    }

    // конструктор инициализирующий
    public Book(string author, string title, int pubYear, int count, int price, string fileName)
    {
        Id = ++_currentMaxId;
        Author = author;
        Title = title;

        PubYear = pubYear;
        Count = count;
        Price = price;
        FileName = fileName;
    }

    #endregion
}
