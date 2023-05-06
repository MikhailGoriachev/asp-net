namespace HomeWork.Models
{
    // Класс Прямоугольник
    public class Rectangle : IFigure
    {
        // id
        private int _id;

        public int Id
        {
            get => _id;
            set
            {
                IFigure.CurrentMaxId = value > IFigure.CurrentMaxId ? value : IFigure.CurrentMaxId;
                _id = value;
            }
        }

        // название
        public string Name => "Прямоугольник";

        // изображение
        public string Image => "rectangle.png";

        // сторона a
        private double _sideA;

        public double SideA
        {
            get => _sideA;
            set => _sideA = value >= 1e-5
                ? value
                : throw new InvalidDataException("Стороны должны быть больше или равны 0.00001!");
        }

        // сторона b

        private double _sideB;

        public double SideB
        {
            get => _sideB;
            set => _sideB = value >= 1e-5
                ? value
                : throw new InvalidDataException("Стороны должны быть больше или равны 0.00001!");
        }

        // периметр
        public double Perimeter => (SideA + SideB) * 2;

        // площадь
        public double Area => SideA * SideB;

        // словарь параметров и их значений
        public Dictionary<string, double> ParamAndValue
        {
            get => new() { ["a"] = SideA, ["b"] = SideB };
        }
    }
}
