namespace Homework.Models;

public class Category
{
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    
    public virtual List<Publication> Publications { get; set; } = new();
}