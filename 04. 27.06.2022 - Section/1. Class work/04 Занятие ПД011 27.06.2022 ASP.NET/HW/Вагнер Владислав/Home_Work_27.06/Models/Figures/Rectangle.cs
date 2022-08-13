using System.Net;
using System.Web;

namespace Home_Work.Models.Figures
{
    public class Rectangle : iFigure
    {
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


        public Rectangle(int a, int b):base("Прямоугольник")
        {
            A = a;
            B = b;
        }

        public override string GetSize()
        {

            return $"Ширина: <b>{_b}</b> <br>" +
                   $"&nbsp;&nbsp;&nbsp;&nbsp;" +
                   $"Высота: <b>{_a}</b>";
        }

        public override int Perimeter()
            => (_a + _b) * 2;

        public override int Area()
            => _a * _b;
    }
}
