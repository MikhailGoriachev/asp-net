using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Models.Figures;
using Home_Work.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_Work.Pages
{
    public class FiguresCollectionModel : PageModel
    {
        //Коллекцкия фигур
        public FiguresSet Set { get; set; } = new FiguresSet();

        //Копия поллекции для сортировок 
        public IEnumerable<IFigure> Figures = new List<IFigure>();

        public void OnGet()
        {
            Figures = Set.Figures;
        }

        #region Сортировки
        //Исходная коллекция
        public void OnGetShowDefault()
        {
            Figures = Set.Figures;
        }

        //Сортировка только квадратов
        public void OnGetSortSquaresByArea()
        {
            Figures = Set.OrderSquaresByAreaDesc();
        }

        //Сортировка только прямоугольников
        public void OnGetSortRectanglesByPerimeter()
        {
            Figures = Set.OrderRectsByPerimeter();
        }

        //Сортировка только прямоугольников
        public void OnGetReverseTriangles()
        {
            Figures = Set.ReverseTriangles();
        }

        //Сортировка коллекции по убыванию площади
        public void OnGetSortBySquare()
        {
            Figures = Set.OrderByAreaDesc();
        }

        //Сортировка коллекции по типу
        public void OnGetSortByType()
        {
            Figures = Set.OrderByType();
        }

        #endregion

        #region Добавление
        /*Добавление записи*/
        public void OnGetAddFigure()
        {
            Set.AddFigure(Utils.GetFigure());

            //Сортировка в обратном порядке
            Figures = Set.Figures.OrderByDescending(f => f.Id);
        }

        #endregion

        //Удаление JSON файла для обновления коллекции 
        public void OnGetDeleteFile()
        {
            Set.DeleteFile();
            Figures = Set.Figures;
        }

    }
}
