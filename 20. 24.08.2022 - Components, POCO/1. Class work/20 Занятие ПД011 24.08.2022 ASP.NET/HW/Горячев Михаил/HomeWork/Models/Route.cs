using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models;

// Класс Маршрут
public class Route
{
    // текущий максимальный id
    private static int _currentMaxId = 0;

    public static int CurrentMaxId
    {
        get => _currentMaxId;
        set => _currentMaxId = value;
    }

    // id
    private int _id;

    [UIHint("HiddenInput")]
    public int Id
    {
        get => _id;
        set
        {
            _currentMaxId = value > _currentMaxId ? value : _currentMaxId;
            _id = value;
        }
    }

    // начальный пункт маршрута
    [Required(ErrorMessage = "Выберите начальный пункт маршрута!")]
    [Display(Name = "Начальный пункт")]
    public string StartPoint { get; set; } = "";

    // обязательный промежуточный пункт маршрута
    [Required(ErrorMessage = "Выберите промежуточный пункт маршрута!")]
    [Display(Name = "Промежуточный пункт")]
    public string MiddlePoint { get; set; } = "";

    // конечный пункт маршрута
    [Required(ErrorMessage = "Выберите конечный пункт маршрута!")]
    [Display(Name = "Конечный пункт")]
    public string EndPoint { get; set; } = "";

    // протяженность маршрута в км (целое число)
    [Required(ErrorMessage = "Введите протяженность маршрута!")]
    [Range(1, int.MaxValue, ErrorMessage = "Протяженность маршрута должна быть больше 0 км!")]
    [Display(Name = "Протяженность маршрута (км)")]
    public int Length { get; set; }

    // сложность маршрута (A-, A, A+, B-, B, B+, C-, C, C+; A- - мин. сложность, C+ макс сложность)
    [Required(ErrorMessage = "Выберите сложность маршрута!")]
    [Display(Name = "Сложность маршрута")]
    public string Difficulty { get; set; } = "";

    // инструктор
    [Required(ErrorMessage = "Выберите инструктора!")]
    [Display(Name = "Инструктор")]
    public int InstructorId { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public Route()
    {

    }

    // конструктор инициализирующий
    public Route(string startPoint, string middlePoint, string endPoint, int length, string difficulty,
        int instructorId)
    {
        _id = ++_currentMaxId;
        StartPoint = startPoint;
        MiddlePoint = middlePoint;
        EndPoint = endPoint;
        Length = length;
        Difficulty = difficulty;
        InstructorId = instructorId;
    }

    #endregion
}
