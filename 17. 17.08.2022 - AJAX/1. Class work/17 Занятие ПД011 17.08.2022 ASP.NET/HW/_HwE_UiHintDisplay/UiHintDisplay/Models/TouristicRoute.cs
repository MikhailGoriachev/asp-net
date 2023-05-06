using System.ComponentModel.DataAnnotations;

namespace UiHintDisplay.Models;

// Сведения о туристических маршрутах содержат: идентификатор маршрута,
// начальный пункт маршрута, обязательный промежуточный пункт маршрута,
// конечный пункт маршрута, протяженность маршрута в км (целое число),
// сложность маршрута (значение из ряда А-, А, А+, В-, В, В+, С-, С, С+;
// А- соответствует минимальной сложности, С+ соответствует максимальной
// сложности), фамилия и инициалы инструктора маршрута (всего на фирме
// работает пять инструкторов, не требуется редактировать сведения об
// инструкторах).
public class TouristicRoute
{
    // идентификатор маршрута
    [UIHint("HiddenInput")]
    public int Id { get; set; }

    // начальный пункт маршрута
    [Display(Name = "Начальный пункт маршрута:")]
    public string StartPoint { get; set; }

    // обязательный промежуточный пункт маршрута
    [Display(Name = "Промежуточный пункт маршрута:")]
    public string MiddlePoint { get; set; }

    // конечный пункт маршрута
    [Display(Name = "Конечный пункт маршрута:")]
    public string EndPoint { get; set; }

    // протяженность маршрута в км
    [Display(Name = "Протяженность маршрута, км:")]
    public int Length { get; set; }

    // сложность маршрута
    [Display(Name = "Сложность маршрута:")]
    public string Complexity { get; set; }

    // инструктор маршрута
    [Display(Name = "Инструктор маршрута:")]
    public int InstructorId { get; set; }


    #region конструкторы
    public TouristicRoute() {
        Id = 0;
        StartPoint = "";
        MiddlePoint = "";
        EndPoint = "";
        Length = 0;
        Complexity = "A-";
        InstructorId = 1;
    } // TouristicRoute


    public TouristicRoute(int id, string startPoint, string middlePoint, 
        string endPoint, int length, string complexity, int instructorId) {
        Id = id;
        StartPoint = startPoint;
        MiddlePoint = middlePoint;
        EndPoint = endPoint;
        Length = length;
        Complexity = complexity;
        InstructorId = instructorId;
    } // TouristicRoute
    #endregion
} // class TouristicRoute

