namespace HomeWork.Models
{
    // Класс Треугольник
    public class Triangle : IFigure
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
        public string Name => "Треугольник";

        // изображение
        public string Image => "triangle.png";

        // стороны треугольника
        private (double A, double B, double C) _sides;

        public (double A, double B, double C) Sides
        {
            get => _sides;
            set => _sides = IsTriangle(value)
                ? value
                : throw new InvalidDataException("Стороны не образуют треугольник!");
        }

        // периметр
        public double Perimeter => _sides.A + _sides.B + _sides.C;

        // площадь
        public double Area
        {
            get
            {
                // полупериметр
                double p = (double)Perimeter / 2;

                return Math.Sqrt(p * (p - _sides.A) * (p - _sides.B) * (p - _sides.C));
            }
        }

        // словарь параметров и их значений
        public Dictionary<string, double> ParamAndValue
        {
            get => new() { ["a"] = _sides.A, ["b"] = _sides.B, ["c"] = _sides.C };
        }


        #region Методы

        // проверка на корректность данных треугольника
        public bool IsTriangle((double a, double b, double c) sides) =>
            sides.a + sides.b > sides.c && sides.b + sides.c > sides.a && sides.c + sides.a > sides.b;

        #endregion
    }
}
