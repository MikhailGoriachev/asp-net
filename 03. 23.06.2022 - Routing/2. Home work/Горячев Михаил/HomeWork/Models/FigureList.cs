using HomeWork.Infrastructure;

namespace HomeWork.Models
{
    // Класс Хранилище фигур
    public class FigureList
    {
        // коллекция фигур
        public List<IFigure> Figures { get; set; }


        #region Конструкторы

        // конструктор по умолчанию
        public FigureList() : this(Utils.GetFigurList(Utils.GetInt(10, 15))) { }


        // конструктор инициализирующий
        public FigureList(List<IFigure> figures)
        {
            // установка значений
            Figures = figures;
        }

        #endregion

        #region Методы

        // выборка квадратов, сортировка по убыванию площади
        public List<IFigure> SelectionBySquare() => Figures.Where(f => f.Name == "Квадрат")
                                                           .OrderByDescending(f => f.Area)
                                                           .ToList();

        // выборка прямоугольников, сортировка по возрастанию периметра
        public List<IFigure> SelectionByRectangle() => Figures.Where(f => f.Name == "Прямоугольник")
                                                              .OrderBy(f => f.Perimeter)
                                                              .ToList();


        // выборка треугольников, в обратном порядке сортировка по отношению к порядку в коллекции
        public List<IFigure> SelectionByTriangle() => Figures.Where(f => f.Name == "Треугольник")
                                                             .Reverse()
                                                             .ToList();


        // сортировка по убыванию площади
        public List<IFigure> OrderByDescArea() => Figures.OrderByDescending(f => f.Area)
                                                         .ToList();

        #endregion
    }
}
