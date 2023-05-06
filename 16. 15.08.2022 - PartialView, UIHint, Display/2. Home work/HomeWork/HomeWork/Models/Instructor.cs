namespace HomeWork.Models;

// Класс Инструктор
public class Instructor
{
    // текущий максимальный id
    private static int _currentMaxId = 0;

    // id
    private int _id;

    public int Id
    {
        get => _id;
        set
        {
            _currentMaxId = value > _currentMaxId ? value : _currentMaxId;
            _id = value;
        }
    }

    // фамилия
    public string Surname { get; set; } = "";

    // имя
    public string Name { get; set; } = "";

    // отчество
    public string Patronymic { get; set; } = "";

    // дата рождения
    public DateTime DateOfBirth { get; set; }

    // категория
    public string Category { get; set; } = "";

    // строчная информация об инструкторе
    public string InstructorToString => $"{Id}. {Surname} {Name} {Patronymic}, категория: {Category}";

    #region Конструкторы

    // конструктор по умолчанию
    public Instructor()
    {
    }


    // конструктор инициализирующий
    public Instructor(string surname, string name, string patronymic, DateTime dateOfBirth, string category)
    {
        Id = ++_currentMaxId;
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        DateOfBirth = dateOfBirth;
        Category = category;
    }

    #endregion
}
