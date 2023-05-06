namespace HomeWork.Models;

// Класс Книга
public class Book
{
    // следующий id
    private static int _nextId = 0;

    // id
    public int Id { get; init; }

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
        Id = _nextId++;
    }

    // конструктор инициализирующий
    public Book(string author, string title, int pubYear, int count, int price, string fileName)
    {
        Id = _nextId++;
        Author = author;
        Title = title;
        PubYear = pubYear;
        Count = count;
        Price = price;
        FileName = fileName;
    }

    #endregion
}
