using RoutesForms.Infrastructure;
using Newtonsoft.Json;

namespace RoutesForms.Models.Task02;

// квадарт
public class Square : IFigure
{
    // размер квадрата - сторона квадрата
    [JsonProperty] // для записи приватного поля в JSON
    private double _a;
    public double A {
        get => _a;
        set => _a = value;
    }


    #region Реализация итерфейса IFigure
    public int Id { get; set; }

    public string Type => "квадрат";

    public string Image => "square.png";

    public double Area() => _a * _a;

    public double Perimeter() => 4 * _a;
    #endregion


    #region Конструкторы
    public Square() : this(1, Utils.GetRandom(1d, 10d)) { } // Rectangle
    public Square(int id) : this(id, Utils.GetRandom(1d, 10d)) { } // Rectangle

    public Square(int id, double a) {
        Id = id;

        if (a <= 0) {
            throw new ArgumentException("Некорректное значение параметра");
        } // if

        _a = a;
    } // Square
    #endregion


    public override string ToString() => $"{_a:f3} x {_a:f3}";
} // class Square
