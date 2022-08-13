using Home_Work.Utilities;

namespace Home_Work.Models.Figures
{
    public class Triangle : IFigure
    {

        public string FigureType => "Треугольник";

        //Сторона А
        private double _a;
        public double A
        {
            get { return _a; }
            set
            {
                if (value < 0)
                    throw new Exception("Сторона не может быть <= 0");

                _a = value;
            }
        }

        //Сторона В
        private double _b;
        public double B
        {
            get { return _b; }
            set
            {
                if (value < 0)
                    throw new Exception("Сторона не может быть <= 0");

                _b = value;
            }
        }

        //Сторона С
        private double _c;
        public double C
        {
            get { return _c; }
            set
            {
                if (value < 0)
                    throw new Exception("Сторона не может быть <= 0");

                _c = value;
            }
        }


        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0)
                    throw new Exception("Id фигуры не может быть = 0");
                _id = value;
            }
        }

        public Triangle(double a, double b, double c)
        {
            if (Triangle.IsTriangel(a,b,c))
            {
                A = a;
                B = b;
                C = c;
            }
            else
            {
                A = Utils.GetRandom(1d,15d);
                B = Utils.GetRandom(A,A+5);
                C = Utils.GetRandom(B,B+5);
            }
        }

        public string GetSize()
        {
            return $"Сторона A: <b>{_a:f2}</b> см<br>" +
                   $"Сторона B: <b>{_b:f2}</b> см<br>" +
                   $"Сторона C: <b>{_c:f2}</b> см";
        }

        public double Perimeter()
            => _a + _b + _c;

        public double Area()
        {
            double p = (_a + _b + _c)/2;
            return Math.Sqrt(p*(p-_a)*(p-_b)*(p-_c));
        }

        //Проверка корректности треугольника
        public static bool IsTriangel(double a, double b, double c) =>
            a + b > c && a + c > b && b + c > a;
    }
}
