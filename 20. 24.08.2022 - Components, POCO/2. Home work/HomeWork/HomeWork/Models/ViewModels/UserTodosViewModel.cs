namespace HomeWork.Models.ViewModels;

// Компонент для вывода дел пользователя
public class UserTodosViewModel
{
    // записи
    public IEnumerable<UserTodo> Data { get; set; }

    // количество записей
    public int Count { get; set; }

    // количество завершенных дел
    public int CountCompleted { get; set; }

    // количество незавершненных дел
    public int CountUnfinished { get; set; }


    // конструктор инициализирующий
    public UserTodosViewModel(IEnumerable<UserTodo> data, int count, int countCompleted, int countUnfinished)
    {
        Data = data;
        Count = count;
        CountCompleted = countCompleted;
        CountUnfinished = countUnfinished;
    }
}
