using BootstrapForms.Infrastructure;
using Newtonsoft.Json;

namespace BootstrapForms.Models.Task02
{
    // квадарт
    public class Square: IFigure
    {
        // размер квадрата - сторона квадрата
        [JsonProperty] // для записи приватного поля в JSON
        private double _a;


        #region Реализация итерфейса IFigure
        public string Type => "квадрат";

        public string Image => "square.png";

        public double Area() => _a * _a;

        public double Perimeter() => 4 * _a;
        #endregion


        #region Конструкторы
        public Square() : this(Utils.GetRandom(1d, 10d))
        { } // Rectangle

        public Square(double a) {
            _a = a;
        } // Square
        #endregion


        public override string ToString() => $"{_a:f3} x {_a:f3}";
    } // class Square
}
