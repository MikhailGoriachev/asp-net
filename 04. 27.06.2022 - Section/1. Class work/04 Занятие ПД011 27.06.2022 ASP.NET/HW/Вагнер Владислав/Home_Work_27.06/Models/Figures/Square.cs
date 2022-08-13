namespace Home_Work.Models.Figures
{
    public class Square : iFigure
    {
        //Высота
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

        public Square(int a) : base("Квадрат")
        {
            A = a;
        }

        public override string GetSize()
        {
            return $"Сторона: <b>{_a}</b>";
        }

        public override int Perimeter()
            => _a*4;

        public override int Area()
            => _a * _a;
    }
}
