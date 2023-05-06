using System.ComponentModel.DataAnnotations;
using Home_work.Infrastructure;

namespace Home_work.Models.Routes;

public class Route
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [Display(Name = "Начальный пункт")]
    //Начальная точка
    public string StartingPoint { get; set; }


    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [Display(Name = "Промежуточный пункт")]
    //Промежуточная точка
    public string MiddlePoint { get; set; }


    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [Display(Name = "Конченый пункт")]
    //Конечная точка
    public string EndPoint { get; set; }


    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [RegularExpression(@"[A-Z][-\+]?", ErrorMessage = "Поле может состоять только из буквы и символа (+-)")]
    [Display(Name = "Уровень сложности маршрута")]
    //Уровень сложности
    public string Complexity { get; set; }

    //Протяженность маршрута
    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [UIHint("Number")]
    [Range(5, 15000, ErrorMessage = "Введите число в диапазоне от 5 км до 15000 км")]
    [Display(Name = "Протяженность маршрута (км)")]
    public int Length { get; set; }


    [Display(Name = "Инструкторы")]
    //Инструктор
    public Instructor InstructorData { get; set; }

    public Route(int id, string startingPoint, string middlePoint, string endPoint, string complexity, int lenghth, Instructor instructorData)
    {
        Id = id;
        StartingPoint = startingPoint;
        MiddlePoint = middlePoint;
        EndPoint = endPoint;
        Complexity = complexity;
        Length = lenghth;
        InstructorData = instructorData;
    }

    //ctor по умолчанию
    public Route() : this(0, "Парк 1", "Озеро N3", "Холм N1",
        Utils.Complexity[Utils.GetRandom(0, Utils.Complexity.Length)],
        6, Utils.GetInstructors()[0])
    {

    }
}
