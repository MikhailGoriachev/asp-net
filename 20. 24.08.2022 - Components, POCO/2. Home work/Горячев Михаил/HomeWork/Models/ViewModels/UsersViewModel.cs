namespace HomeWork.Models.ViewModels;

// Компонент для вывода всех пользователей
public class UsersViewModel
{
    // записи
    public IEnumerable<User> Data { get; set; }

    // количество записей
    public int Count { get; set; }


    // конструктор инициализируюий
    public UsersViewModel(IEnumerable<User> data, int count)
    {
        Data = data;
        Count = count;
    }
}
