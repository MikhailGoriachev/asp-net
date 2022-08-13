using System.Net;
using System.Web;

namespace Home_Work.Models.Figures
{
    public class Rectangle : IFigure
    {
        public string FigureType => "Прямоугольник";

        //Высота
        private int _a;
        public int A
        {
            get { return _a; }
            set {
                if (value < 0)
                    throw new Exception("Высота не может быть <= 0");

                _a = value; 
            }
        }

        //Ширина
        private int _b;
        public int B
        {
            get { return _b; }
            set {
                if (value < 0)
                    throw new Exception("Ширина не может быть <= 0");

                _b = value; 
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

        public Rectangle(int a, int b)
        {
            A = a;
            B = b;
        }

        public string GetSize()
        {

            return $"Ширина: <b>{_b}</b><br> " +
                   $"Высота: <b>{_a}</b>";
        }

        public int Perimeter()
            => (_a + _b) * 2;

        public int Area()
            => _a * _b;
    }
}
