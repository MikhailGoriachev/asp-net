using RoutesForms.Infrastructure;
using Newtonsoft.Json;

namespace RoutesForms.Models.Task02;

public class Rectangle : IFigure
{

    // размер прямоугольника - стороны прямоугольника
    [JsonProperty] // для записи приватного поля в JSON
    private double _a;
    public double A {
        get => _a;
        set => _a = value;
    }

    [JsonProperty] // для записи приватного поля в JSON
    private double _b;
    public double B {
        get => _b;
        set => _b = value;
    }



    #region Реализация итерфейса IFigure
    public int Id { get; set; }

    public string Type => "прямоугольник";

    public string Image => "rectangle.png";

    public double Area() => _a * _b;

    public double Perimeter() => 2 * (_a + _b);

    #endregion


    #region Конструкторы
    public Rectangle() : this(1, Utils.GetRandom(1d, 10d), Utils.GetRandom(1d, 10d)) { } // Rectangle

    public Rectangle(int id) : this(id, Utils.GetRandom(1d, 10d), Utils.GetRandom(1d, 10d)) { } // Rectangle

    public Rectangle(int id, double a, double b) {
        Id = id;

        if (a <= 0 || b <= 0) {
            throw new ArgumentException("Некорректные значения параметров");
        } // if

        _a = a;
        _b = b;
    } // Rectangle
    #endregion


    public override string ToString() => $"{_a:f3} x {_b:f3}";
} // class Rectangle
