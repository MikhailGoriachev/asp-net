using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Models.Figures;
using Home_Work.Utilities;

namespace Home_Work.Pages
{
    public class FiguresCollectionModel : PageModel
    {
        //Коллекцкия фигур
        public List<iFigure> Figures { get; set; } = new List<iFigure>()
            {
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
            };

        //Копия поллекции для сортировок 
        public List<iFigure> FiguresCopy = new List<iFigure>();

        //Флаг проверки типа обработчика OnGet
        public bool IsDefault = true;

        public void OnGet()
        {
            /*Figures = new List<iFigure>()
            {
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
                Utils.GetFigure(),
            };

            //Добавление копии для сортировок и выборок
            FiguresCopy = Figures.Select((f) => f).ToList();*/
        }

        //Сортировка только квадатов
        public void OnGetSortSquaresByArea()
        {
            IsDefault = false;
            FiguresCopy.AddRange(Figures
                .Where((f) => f.FigureType.ToLower().Contains("квад"))
                .OrderByDescending(f => f.Area()));
        }

        //Сортировка только прямоугольников
        public void OnGetSortRectanglesByPerimeter()
        {
            IsDefault = false;
            FiguresCopy = Figures.Where((f) => f.FigureType.ToLower().Contains("прямо")).ToList();
            FiguresCopy.Sort((s1, s2) => s1.Perimeter() - s2.Perimeter());
        }

        //Сортировка только прямоугольников
        public void OnGetReverseTriangles()
        {
            IsDefault = false;
            FiguresCopy = Figures
                .Where((f) => f.FigureType.ToLower().Contains("треугольник"))
                .Reverse()
                .ToList();
        }
        //Сортировка коллекции повозрастанию площади
        public void OnGetSortBySquare()
        {
            Figures.Sort((f1, f2) => f1.Area() - f2.Area());
        }


        /*Добавление записи*/
        public void OnPost()
        {
            Figures.Add(Utils.GetFigure());
            //Добавление копии для сортировок и выборок
            FiguresCopy = Figures.Select((f) => f).ToList();
        }
    }
}
