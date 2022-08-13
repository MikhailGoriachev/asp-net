using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    public class FiguresModel : PageModel
    {
        // хранилище фигур
        public static FigureList FigureSource { get; set; } = new FigureList();

        // текущая отображаемая коллекция
        public List<IFigure> CurrentFigures { get; set; } = FigureSource.Figures;

        // название обработки
        public string NameHandler { get; set; } = "Исходная последовательность";


        #region Методы

        // исходная последовательность
        public void OnGet()
        {

        }


        // сортировка по убыванию площади
        public void OnGetOrderByDescArea()
        {
            NameHandler = "Сортировка по убыванию площади";
            CurrentFigures = FigureSource.OrderByDescArea();
        }


        // выборка квадратов, сортировка по убыванию площади
        public void OnGetSelectBySquare()
        {
            NameHandler = "Выборка квадратов, сортировка по убыванию площади";
            CurrentFigures = FigureSource.SelectionBySquare();
        }


        // выборка прямоугольников, сортировка по возрастанию периметра
        public void OnGetSelectByRect()
        {
            NameHandler = "Выборка прямоугольников, сортировка по возрастанию периметра";
            CurrentFigures = FigureSource.SelectionByRectangle();
        }


        // выборка треугольников, в обратном порядке сортировка по отношению к порядку в коллекции
        public void OnGetSelectByTriangle()
        {
            NameHandler = "Выборка треугольников, в обратном порядке сортировка по отношению к порядку в коллекции";
            CurrentFigures = FigureSource.SelectionByTriangle();
        }


        // добавление фигуры
        public void OnPostAddFigure()
        {
            NameHandler = "Фигура добавлена в начало";
            FigureSource.Figures.Insert(0, Utils.GetFigure());
            CurrentFigures = FigureSource.Figures;
        }

        #endregion
    }
}
