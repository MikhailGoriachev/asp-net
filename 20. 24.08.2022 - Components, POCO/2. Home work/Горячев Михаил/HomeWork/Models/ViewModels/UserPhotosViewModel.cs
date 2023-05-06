namespace HomeWork.Models.ViewModels;

// Модель представления для вывода фотографий пользователя
public class UserPhotosViewModel
{
    // записи
    public IEnumerable<UserPhoto> Data { get; set; }

    // количество записей
    public int Count { get; set; }


    // конструктор инициализирующий
    public UserPhotosViewModel(IEnumerable<UserPhoto> data, int count)
    {
        Data = data;
        Count = count;
    }
}
