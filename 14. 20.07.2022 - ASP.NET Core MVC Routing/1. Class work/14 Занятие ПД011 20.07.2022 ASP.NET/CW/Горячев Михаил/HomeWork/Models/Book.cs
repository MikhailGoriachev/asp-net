namespace HomeWork.Models;

// Класс Книга
public record Book(string Author, string Title, int PubYear, int Count, string FileName)
{
    // слудующий id
    private static int _nextId = 0;

    // ID
    public int Id { get; init; } = _nextId++;
}
