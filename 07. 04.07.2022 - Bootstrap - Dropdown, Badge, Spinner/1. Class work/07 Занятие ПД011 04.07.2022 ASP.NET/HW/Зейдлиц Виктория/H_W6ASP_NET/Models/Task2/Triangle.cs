using H_W6ASP_NET.Infrastructure;

namespace H_W6ASP_NET.Models.Task02
{
    public class Triangle: IFigure
    {

        // стороны треугольника
        private (double A, double B, double C) _sides;
        public (double A, double B, double C) Sides
        {
            get => _sides;
            set => _sides = IsTriangle(value)
                            ? value
                            : throw new InvalidDataException("Стороны не образуют треугольник!");
        }

        #region Интерфейс IFigure

        public string Type => "треугольник";

        public string Image => "triangle.png";


        public double Area()
        {
            double p = Perimeter() / 2;
            return Math.Sqrt(p * (p - _sides.A) * (p - _sides.B) * (p - _sides.C));
        } // Area

        public double Perimeter() => _sides.A + _sides.B + _sides.C;

        #endregion


        #region Конструкторы

        public Triangle() { }

        public Triangle((double a, double b, double c) side)
        {
            _sides.A = side.a;
            _sides.B = side.b;
            _sides.C = side.c;
        } // Triangle

        #endregion


        // проверка треугольника на существование
        public static bool IsTriangle((double a, double b, double c) sides) =>
            sides.a + sides.b > sides.c && sides.a + sides.c > sides.b && sides.b + sides.c > sides.a;

        public override string ToString() => $"{_sides.A:f3} x {_sides.B:f3} x {_sides.C:f3}";

    } // class Triangle
}
