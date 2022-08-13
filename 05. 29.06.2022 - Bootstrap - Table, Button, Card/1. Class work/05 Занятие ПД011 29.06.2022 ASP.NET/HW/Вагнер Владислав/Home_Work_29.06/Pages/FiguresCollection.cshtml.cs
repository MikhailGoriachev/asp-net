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

        /*[BindProperty]*/
        //Фигура для добавления через форму
        //public IFigure Figure { get; set; }

        //Список для выпадающего списка
        public SelectList types = new SelectList(new List<IFigure>());

        //Загрузка списка в форме доавбления
        public void LoadDropDown()
        {
            types = new SelectList(Set.Figures.DistinctBy(f => f.FigureType), "FigureType", "FigureType");
        }
        public void OnGet()
        {
            Figures = Set.Figures;
            /*types = new SelectList(Figures.DistinctBy(f => f.FigureType), "FigureType", "FigureType");*/
            LoadDropDown();
        }

        #region Сортировки
        //Исходная коллекция
        public void OnPostShowDefault()
        {
            Figures = Set.Figures;
            LoadDropDown();
        }

        //Сортировка только квадратов
        public void OnPostSortSquaresByArea()
        {
            LoadDropDown();
            Figures = Set.OrderSquaresByAreaDesc();
        }

        //Сортировка только прямоугольников
        public void OnPostSortRectanglesByPerimeter()
        {
            LoadDropDown();
            Figures = Set.OrderRectsByPerimeter();
        }

        //Сортировка только прямоугольников
        public void OnPostReverseTriangles()
        {
            LoadDropDown();
            Figures = Set.ReverseTriangles();
        }

        //Сортировка коллекции по убыванию площади
        public void OnPostSortBySquare()
        {
            LoadDropDown();
            Figures = Set.OrderByAreaDesc();
        }

        //Сортировка коллекции по типу
        public void OnPostSortByType()
        {
            Figures = Set.OrderByType();
            LoadDropDown();
        }

        #endregion

        #region Добавление
        /*Добавление записи*/
        public void OnPostAddFigure()
        {
            Set.AddFigure(Utils.GetFigure());

            //Сортировка в обратном порядке
            Figures = Set.Figures.OrderByDescending(f => f.Id);
            LoadDropDown();
        }

        /*Добавление записи из формы*/
        public void OnPostAddFigureForm()
        {
            //Создание объекта
            IFigure Figure = Request.Form["Type"].ToString().ToLower() switch
            {
                "квадрат" => new Square(int.Parse(Request.Form["SideA"])),
                "прямоугольник" => new Rectangle(int.Parse(Request.Form["SideA"]), int.Parse(Request.Form["SideB"])),
                _ => new Triangle(int.Parse(Request.Form["SideA"]), int.Parse(Request.Form["SideB"]), int.Parse(Request.Form["SideC"])),
            };

            //Задание корректного ID
            Figure.Id = Set.Figures.Max(f => f.Id) + 1;
            Utils.LastFigureId++;

            //Если запись создать не удастся, то добавится рандомная фигура
            Set.AddFigure(Figure ?? Utils.GetFigure());

            //Сортировка в обратном порядке 
            Figures = Set.Figures.OrderByDescending(f => f.Id);
            LoadDropDown();
        }
        #endregion

        //Удаление JSON файла для обновления коллекции 
        public void OnPostDeleteFile()
        {
            Set.DeleteFile();
            Figures = Set.Figures;
            LoadDropDown();
        }

    }
}
