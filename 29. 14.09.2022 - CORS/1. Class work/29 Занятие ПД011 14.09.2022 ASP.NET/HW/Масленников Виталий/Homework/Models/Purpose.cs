namespace Homework.Models;

public class Purpose
{
    public int Id { get; set; }
    
    // Цель поездки
    public string? Name { get; set; }
    
    public List<Route> Routes { get; set; } = new();
}