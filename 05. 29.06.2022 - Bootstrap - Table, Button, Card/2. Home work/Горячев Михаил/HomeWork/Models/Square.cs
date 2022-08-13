namespace HomeWork.Models
{
    // Класс Квадрат
    public class Square : IFigure
    {
        // название
        public string? Name => "Квадрат";

        // изображение
        public string? Image => "square.png";

        // сторона
        public double Side { get; set; }

        // периметр
        public double Perimeter => Side * 4;

        // площадь
        public double Area => Side * Side;

        // словарь параметров и их значений
        public Dictionary<string, double> ParamAndValue { get => new() { ["a"] = Side }; }

    }
}
