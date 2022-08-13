using System.Net;
using System.Web;

namespace Home_Work.Models.Figures
{
    public class Rectangle : IFigure
    {
        public string FigureType => "Прямоугольник";

        //Высота
        private double _a;
        public double A
        {
            get { return _a; }
            set {
                if (value < 0)
                    throw new Exception("Высота не может быть <= 0");

                _a = value; 
            }
        }

        //Ширина
        private double _b;
        public double B
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

        public Rectangle(double a, double b)
        {
            A = a;
            B = b;
        }

        public string GetSize()
        {

            return $"Ширина: <b>{_b:f2}</b> см<br> " +
                   $"Высота: <b>{_a:f2}</b> см";
        }

        public double Perimeter()
            => (_a + _b) * 2;

        public double Area()
            => _a * _b;
    }
}
