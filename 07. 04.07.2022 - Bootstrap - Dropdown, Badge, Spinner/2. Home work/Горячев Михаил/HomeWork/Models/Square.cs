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
        private double _side;

        public double Side
        {
            get => _side;
            set => _side = value >= 1e-5 ? value
                                : throw new InvalidDataException("Стороны должны быть больше или равны 0.00001!");
        }

        // периметр
        public double Perimeter => Side * 4;

        // площадь
        public double Area => Side * Side;

        // словарь параметров и их значений
        public Dictionary<string, double> ParamAndValue => new() { ["a"] = Side }; 

    }
}
