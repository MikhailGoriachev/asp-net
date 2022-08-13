using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Pages
{
    public class FiguresModel : PageModel
    {
        // хранилище фигур
        public FigureList FigureSource { get; set; } = new FigureList();

        // текущая отображаемая коллекция
        public List<IFigure> CurrentFigures { get; set; } = new List<IFigure>();

        // название обработки
        public string NameHandler { get; set; } = "Исходная последовательность";

        // сторона a
        [BindProperty]
        public double SideA { get; set; }

        // сторона b
        [BindProperty]
        public double SideB { get; set; }

        // сторона c
        [BindProperty]
        public double SideC { get; set; }

        // тип фигуры
        [BindProperty]
        public string? FigureType { get; set; }

        // сообщение об ошибке добавления
        public string? ErrorMessage { get; set; }

        #region Методы

        // исходная последовательность
        public void OnGet()
        {
            CurrentFigures = FigureSource.Figures;
        }


        // сортировка по убыванию площади
        public void OnGetOrderByDescArea()
        {
            NameHandler = "Сортировка по убыванию площади";
            CurrentFigures = FigureSource.OrderByDescArea();
        }


        // сортировка по типу фигуры
        public void OnGetOrderByName()
        {
            NameHandler = "Сортировка по типу фигуры";
            CurrentFigures = FigureSource.OrderByName();
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
            try
            {
                // установка текущей коллекции
                CurrentFigures = FigureSource.Figures;

                // вставка в начало коллекции
                FigureSource.Figures.Insert(0, FigureType switch
                {
                    "Квадрат" => new Square() { Side = SideA },
                    "Прямоугольник" => new Rectangle() { SideA = SideA, SideB = SideB },
                    _ => new Triangle() { Sides = (SideA, SideB, SideC) }
                });

                // сохранение данных
                FigureSource.Serialization();

                // установка заголовка
                NameHandler = "Фигура добавлена в начало";

                // обнуление типа выбранной фигуры (чтоб спрятать форму добавления)
                FigureType = null;
            }
            catch (Exception ex)
            {
                // установка сообщения об ошибке
                ErrorMessage = ex.Message;
            }

        }


        // открытие формы для создания фигуры
        public void OnPostOpenFigureForm() =>
            CurrentFigures = FigureSource.Figures;


        // отмена создания фигуры
        public void OnPostCancelAddFigure()
        {
            CurrentFigures = FigureSource.Figures;

            // обнуление типа выбранной фигуры (чтоб спрятать форму добавления)
            FigureType = null;
        }

        #endregion
    }
}
