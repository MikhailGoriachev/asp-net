namespace RoutesForms.Models.Task01;

// Информация о книге в библиотеке по заданию
// Сведения о книге в библиотеке: идентификатор книги, фамилия и инициалы автора,
// название, год издания, количество экземпляров данной книги в библиотеке,
// цена одного экземпляра, имя файла с изображением обложки.
public class Book // класс сгенерирован из record
{
    // свойства класса
    public int Id        { get; init; }
    public string Author { get; init; }
    public string Title  { get; init; }
    public int PubYear   { get; init; }
    public int Quantity  { get; init; }
    public int Cost      { get; init; }
    public string Cover  { get; init; }


    // Обязательный конструктор для привязчика типов ASP.NET
    public Book() {
        this.Id = 0;
        this.Author = "";
        this.Title = "";
        this.PubYear = DateTime.Now.Year;
        this.Quantity = 1;
        this.Cost = 1;
        this.Cover = "cover012.jpg";
    }

    public Book(int id, string author, string title, int pubYear, 
        int quantity, int cost, string cover) {
        this.Id = id;
        this.Author = author;
        this.Title = title;
        this.PubYear = pubYear;
        this.Quantity = quantity;
        this.Cost = cost;
        this.Cover = cover;
    } // Book


    public void Deconstruct(out int id, out string author, out string title, out int pubYear, 
        out int quantity, out int cost, out string cover) {
        id = this.Id;
        author = this.Author;
        title = this.Title;
        pubYear = this.PubYear;
        quantity = this.Quantity;
        cost = this.Cost;
        cover = this.Cover;
    } // Deconstruct
} // class Book
