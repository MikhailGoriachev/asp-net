namespace Homework.Models.Books;

public interface IBooksRepository
{
    IEnumerable<Book> Books { get; }
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int id);
    void SaveData();
}