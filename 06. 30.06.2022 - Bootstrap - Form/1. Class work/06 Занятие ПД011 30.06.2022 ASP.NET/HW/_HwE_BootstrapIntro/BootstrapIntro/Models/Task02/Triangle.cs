using BootstrapIntro.Infrastructure;

namespace BootstrapIntro.Models.Task02
{
    public class Triangle: IFigure
    {
        // стороны треугольника
        private double _a;
        private double _b;
        private double _c;


        #region Реализация интерфейса IFigure

        public string Type => "треугольник";

        public string Image => "triangle.png";


        public double Area() {
            double p = Perimeter() / 2;
            return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
        } // Area

        public double Perimeter() => _a + _b + _c;

        #endregion


        #region Конструкторы

        public Triangle():this(3, 4 ,5) { }

        public Triangle(double a, double b, double c) {
            if (IsTriangel(a, b, c)) {
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
        public static bool IsTriangel(double a, double b, double c) => 
            a + b > c && a + c > b && b + c > a;

        public override string ToString() => $"{_a:f3} x {_b:f3} x {_c:f3}";
    } // class Triangle
}
