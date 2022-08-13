namespace Home_Work.Models.Figures
{
    //Базовый абстрактный класс фигуры
    abstract public class iFigure
    {
        //Тип фигуры
        private string _figureType;
        public string FigureType
        {
            get { return _figureType; }
            set { _figureType = value; }
        }

        public iFigure(string figureType)
        {
            FigureType = figureType;
        }

        //Площадь фигуры
        public abstract int Area();

        //Перметр 
        public abstract int Perimeter();

        //Размеры фигуры
        public abstract string GetSize();
    }
}
