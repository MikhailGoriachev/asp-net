namespace HomeWork.Models;

// Класс Запланированное дело пользователя
public class UserTodo
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
}
