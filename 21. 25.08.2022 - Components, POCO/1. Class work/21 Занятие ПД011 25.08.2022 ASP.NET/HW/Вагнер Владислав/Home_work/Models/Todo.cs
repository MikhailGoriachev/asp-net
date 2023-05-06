using System.ComponentModel.DataAnnotations;

namespace Home_work.Models;

public class Todo
{
    //Id списка дел
    public int Id { get; set; }

    //Id пользователя
    public int UserId { get; set; }

    //Заголовок
    public string Title { get; set; }

    //Признак завершенности
    public bool Completed { get; set; }
}
