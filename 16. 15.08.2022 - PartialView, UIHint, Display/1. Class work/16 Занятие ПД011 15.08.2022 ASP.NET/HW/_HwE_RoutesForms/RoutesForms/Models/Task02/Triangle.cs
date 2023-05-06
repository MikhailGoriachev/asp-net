using Newtonsoft.Json;

namespace RoutesForms.Models.Task02;

public class Triangle : IFigure
{
    // стороны треугольника
    [JsonProperty] // для записи приватного поля в JSON
    private double _a;
    public double A => _a;

    [JsonProperty] // для записи приватного поля в JSON
    private double _b;
    public double B => _b;

    [JsonProperty] // для записи приватного поля в JSON
    private double _c;
    public double C => _c;

    // Защищенное присваивание сторонам треугольника
    public (double A, double B, double C) Sides {
        get => (_a, _b, _c);
        set {
            if (IsTriangle(value.A, value.B, value.C)) {
                (_a, _b, _c) = value;
            } // if
        } // set
    } // Sides


    #region Реализация интерфейса IFigure
    public int Id { get; set; }
    public string Type => "треугольник";

    public string Image => "triangle.png";


    public double Area() {
        double p = Perimeter() / 2;
        return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
    } // Area

    public double Perimeter() => _a + _b + _c;

    #endregion


    #region Конструкторы

    public Triangle() : this(1, 3, 4, 5) { }
    public Triangle(int id) : this(id, 3, 4, 5) { }

    public Triangle(int id, double a, double b, double c) {
        Id = id;

        if (IsTriangle(a, b, c)) {
            _a = a;
            _b = b;
            _c = c;
        } else {
            // не лучшее решение :( првильнее - исключение бросать
            //a = Utils.GetRandom(1d, 10d);
            //b = Utils.GetRandom(1d, 10d); ;
            //c = a + b + Utils.GetRandom(3d, 5d);
            throw new InvalidDataException("Недопустимое значение сторон треугольника");
        } // i
    } // Triangle

    #endregion


    // проверка треугольника на существование
    public static bool IsTriangle(double a, double b, double c) =>
        a + b > c && a + c > b && b + c > a;

    public override string ToString() => $"{_a:f3} x {_b:f3} x {_c:f3}";
} // class Triangle

