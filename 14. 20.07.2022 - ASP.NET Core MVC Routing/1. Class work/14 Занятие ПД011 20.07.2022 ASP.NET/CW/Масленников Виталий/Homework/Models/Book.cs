namespace Homework.Models;

public class Book
{
    public int Id { get; set; }

    public string Image { get; set; } = null!;
    
    public string Author { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public int Year { get; set; }
    
    public int Amount { get; set; }

    public Book(int id, string title, string author, string image, int year, int amount)
    {
        Id = id;
        Title = title;
        Author = author;
        Image = image;
        Year = year;
        Amount = amount;
    }
}