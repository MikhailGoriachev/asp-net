

using H_W6ASP_NET.Infrastructure;

namespace H_W6ASP_NET.Models.Task02
{
    public class Rectangle: IFigure
    {

        // размер прямоугольника - стороны прямоугольника
        private double _a;
        private double _b;


        public double A { get; set; }

        public double B { get; set; }


        #region Итерфейс IFigure

        public string Type => "прямоугольник";

        public string Image => "rectangle.png";

        public double Area() => _a * _b;

        public double Perimeter() => 2 * (_a + _b);

        #endregion


        #region Конструкторы
        public Rectangle() : this(Utils.GetRandom(1d, 10d), Utils.GetRandom(1d, 10d))
        { } // Rectangle

        public Rectangle(double a, double b){
            _a = a;
            _b = b;
        } // Rectangle
        #endregion


        public override string ToString() => $"{_a:f3} x {_b:f3}";
    } // class Rectangle
}
