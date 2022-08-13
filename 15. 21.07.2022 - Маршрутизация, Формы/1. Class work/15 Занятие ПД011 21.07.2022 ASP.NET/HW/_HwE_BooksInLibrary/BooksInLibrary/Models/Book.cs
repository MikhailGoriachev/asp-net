namespace BooksInLibrary.Models;

// Информация о книге в библиотеке по заданию
// Сведения о книге в библиотеке: идентификатор книги, фамилия и инициалы автора,
// название, год издания, количество экземпляров данной книги в библиотеке,
// цена одного экземпляра, имя файла с изображением обложки.
public record Book(
    int Id, string Author, string Title, int PubYear, 
    int Quantity, int Cost, string Cover
);

