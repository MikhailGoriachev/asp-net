using BootstrapForms.Infrastructure;
using Newtonsoft.Json;

namespace BootstrapForms.Models.Task02
{
    public class Rectangle: IFigure
    {

        // размер прямоугольника - стороны прямоугольника
        [JsonProperty] // для записи приватного поля в JSON
        private double _a;
        
        [JsonProperty] // для записи приватного поля в JSON
        private double _b;


        #region Реализация итерфейса IFigure

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
