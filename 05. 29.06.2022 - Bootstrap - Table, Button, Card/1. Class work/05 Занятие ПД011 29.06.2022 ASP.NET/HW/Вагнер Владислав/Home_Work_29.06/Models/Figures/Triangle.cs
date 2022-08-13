using Home_Work.Utilities;

namespace Home_Work.Models.Figures
{
    public class Triangle : IFigure
    {

        public string FigureType => "Треугольник";

        //Сторона А
        private int _a;
        public int A
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
        private int _b;
        public int B
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
        private int _c;
        public int C
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

        public Triangle(int a, int b, int c)
        {
            if (Triangle.IsTriangel(a,b,c))
            {
                A = a;
                B = b;
                C = c;
            }
            else
            {
                A = Utils.GetRandom(1,15);
                B = Utils.GetRandom(A,A+5);
                C = Utils.GetRandom(B,B+5);
            }
        }

        public string GetSize()
        {
            return $"Сторона A: <b>{_a}</b><br>" +
                   $"&nbsp;&nbsp;&nbsp;&nbsp;" +
                   $"Сторона B: <b>{_b}</b><br>" +
                   $"&nbsp;&nbsp;&nbsp;&nbsp;" +
                   $"Сторона C: <b>{_c}</b>";
        }

        public int Perimeter()
            => _a + _b + _c;

        public int Area()
        {
            int p = (_a + _b + _c)/2;
            return (int)Math.Sqrt(p*(p-_a)*(p-_b)*(p-_c));
        }

        //Проверка корректности треугольника
        public static bool IsTriangel(double a, double b, double c) =>
            a + b > c && a + c > b && b + c > a;
    }
}
