namespace HomeWork.Models
{
    // Класс Прямоугольник
    public class Rectangle : IFigure
    {
        // название
        public string? Name => "Прямоугольник";

        // изображение
        public string? Image => "rectangle.png";

        // сторона a
        public double SideA { get; set; }

        // сторона b
        public double SideB { get; set; }

        // периметр
        public double Perimeter => (SideA + SideB) * 2;

        // площадь
        public double Area => SideA * SideB;

        // словарь параметров и их значений
        public Dictionary<string, double> ParamAndValue { get => new() { ["a"] = SideA, ["b"] = SideB }; }
    }
}
