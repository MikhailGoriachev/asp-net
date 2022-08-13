namespace BooksInLibrary.Models
{
    // Информация о книге в библиотеке по заданию
    public record Book(int Id, string Author, string Title, int PubYear, int Quantity, string Cover);
}
