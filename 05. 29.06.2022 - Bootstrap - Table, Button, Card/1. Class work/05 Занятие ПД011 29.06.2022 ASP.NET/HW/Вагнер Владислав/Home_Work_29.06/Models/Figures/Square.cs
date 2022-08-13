namespace Home_Work.Models.Figures
{
    public class Square : IFigure
    {

        public string FigureType => "Квадрат";
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

        public Square(int a)
        {
            A = a;
        }

        public string GetSize()
        {
            return $"Сторона: <b>{_a}</b>";
        }

        public int Perimeter()
            => _a*4;

        public int Area()
            => _a * _a;
    }
}
