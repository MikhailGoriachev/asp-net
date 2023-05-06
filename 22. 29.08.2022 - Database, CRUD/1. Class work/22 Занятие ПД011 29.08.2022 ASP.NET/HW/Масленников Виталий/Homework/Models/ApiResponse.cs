namespace Homework.Models;

// Модель для возвращаемой swapi.dev страницы данных
public class ApiResponse<T>
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public List<T>? Results { get; set; }
}