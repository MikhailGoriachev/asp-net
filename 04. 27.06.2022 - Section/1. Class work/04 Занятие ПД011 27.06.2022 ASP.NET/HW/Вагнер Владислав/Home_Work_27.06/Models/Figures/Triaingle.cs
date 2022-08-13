namespace Home_Work.Models.Figures
{
    public class Triaingle : iFigure
    {
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


        public Triaingle(int a, int b, int c) : base("Треугольник")
        {
            A = a;
            B = b;
            C = c;
        }

        public override string GetSize()
        {
            return $"Сторона A: <b>{_a}</b><br>" +
                   $"&nbsp;&nbsp;&nbsp;&nbsp;" +
                   $"Сторона B: <b>{_b}</b><br>" +
                   $"&nbsp;&nbsp;&nbsp;&nbsp;" +
                   $"Сторона C: <b>{_c}</b>";
        }

        public override int Perimeter()
            => _a + _b + _c;

        public override int Area()
        {
            int p = (_a + _b + _c)/2;
            return (int)Math.Sqrt(p*(p-_a)*(p-_b)*(p-_c));
        }    
    }
}
