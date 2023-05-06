using Homework.Models.Books;

namespace Homework.Models.ViewModels;

public class LibraryIndexViewModel
{
    public IEnumerable<Book> Books { get; set; } = null!;
    public Book? Book { get; set; } 
    public bool InvokeForm { get; set; }
    public bool IsEdit { get; set; }
    public IFormFile? File { get; set; }
    public string? ErrMsg { get; set; }
}