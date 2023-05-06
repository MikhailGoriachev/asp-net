namespace HomeWork.Models.ViewModels;

// Модель представления для вывода постов пользователя
public class UserPostsViewModel
{
    // записи
    public IEnumerable<UserPost> Data { get; set; }

    // количество записей
    public int Count { get; set; }

    // минимальная длина поста
    public int MinLength { get; set; }

    // средняя длина поста
    public int AvgLength { get; set; }

    // максимальная длина поста
    public int MaxLength { get; set; }


    // конструктор инициализирующий
    public UserPostsViewModel(IEnumerable<UserPost> data, int count, int minLength, int avgLength, int maxLength)
    {
        Data = data;
        Count = count;
        MinLength = minLength;
        AvgLength = avgLength;
        MaxLength = maxLength;
    }
}
