namespace HomeWork.Models;

// Класс Модель маршрута для вывода
public class RouteViewModel
{
    // id
    public int Id { get; set; }

    // начальный пункт маршрута
    public string StartPoint { get; set; } = "";

    // обязательный промежуточный пункт маршрута
    public string MiddlePoint { get; set; } = "";

    // конечный пункт маршрута
    public string EndPoint { get; set; } = "";

    // протяженность маршрута в км (целое число)
    public int Length { get; set; }

    // сложность маршрута (A-, A, A+, B-, B, B+, C-, C, C+; A- - мин. сложность, C+ макс сложность)
    public string Difficulty { get; set; } = "";

    // инструктор ID
    public int InstructorId { get; set; }

    // инструктор
    public Instructor? Instructor { get; set; }


    #region Конструкторы

    // конструктор по умолчанию
    public RouteViewModel()
    {

    }


    // конструктор инициализирующий
    public RouteViewModel(Route route, Instructor instructor)
    {
        Id = route.Id;
        StartPoint = route.StartPoint;
        MiddlePoint = route.MiddlePoint;
        EndPoint = route.EndPoint;
        Length = route.Length;
        Difficulty = route.Difficulty;
        InstructorId = route.InstructorId;
        Instructor = instructor;
    }

    #endregion
}
