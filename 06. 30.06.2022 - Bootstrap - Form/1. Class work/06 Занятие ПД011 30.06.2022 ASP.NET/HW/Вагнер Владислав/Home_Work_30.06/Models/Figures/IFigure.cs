namespace Home_Work.Models.Figures
{
    //
    public interface IFigure
    {
        //Тип фигуры
        public string FigureType
        {
            get;
        }

        //Идентификатор
        public int Id
        {
            get;
            set;
        }

        //Площадь фигуры
        public int Area();

        //Перметр 
        public int Perimeter();

        //Размеры фигуры
        public string GetSize();
    }
}
