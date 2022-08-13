using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;
using Home_Work.Models.Figures;
using Home_Work.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_Work.Pages
{
    public class Figures : PageModel
    {
        //Коллекцкия фигур
        public FiguresCollection collection { get; set; } = new FiguresCollection();

        //Копия поллекции для сортировок 
        public IEnumerable<IFigure> figures = new List<IFigure>();

        //Фигура для добавляения. FormFigure - статический объект в Utils, поскольку при отработке 1-го onPost происходит 
        //перезагрузка страницы и объект пересоздаётся
        public IFigure AddingFigure { get; set; } = Utils.FormFigure;


        //Список для выпадающего списка
        public SelectList types = new SelectList(new List<IFigure>());


        //Тип формы 
        public string FormType = "dropDown";

        public void OnGet()
        {
            FormType = "dropDown";
            figures = collection.Figures;
        }

        #region Сортировки
        //Исходная коллекция
        public void OnGetShowDefault() 
            => figures = collection.Figures;

        //Сортировка только квадратов
        public void OnGetSortSquaresByArea()
            => figures = collection.OrderSquaresByAreaDesc();

        //Сортировка только прямоугольников
        public void OnGetSortRectanglesByPerimeter()
            =>figures = collection.OrderRectsByPerimeter();

        //Треугольники в обратном порядке
        public void OnGetReverseTriangles()
            => figures = collection.ReverseTriangles();
            

        //Сортировка коллекции по убыванию площади
        public void OnGetSortBySquare()
            => figures = collection.OrderByAreaDesc();

        //Сортировка коллекции по типу
        public void OnGetSortByType()
            => figures = collection.OrderByType();

        #endregion

        #region Добавление
        /*Добавление записи (случайная фигура)*/
        public void OnGetAddFigure()
        {
            collection.AddFigure(Utils.GetFigure());

            //Сортировка в обратном порядке
            figures = collection.Figures.OrderByDescending(f => f.Id);
        }

        //Выбор типа фигуры
        public void OnPostSelectType()
        {
            Utils.FormFigure = Request.Form["figureType"].ToString().ToLower() switch
            {
                "квадрат" => new Square(0),
                "прямоугольник" => new Rectangle(0d, 0d),
                _ => new Triangle(0d, 0d, 0d),
            };

            AddingFigure = Utils.FormFigure;

            //Меняем режим работы формы
            FormType = "parameters_input";
            
            figures = collection.Figures;
        }

        //Добавление параметров фигуры
        public void OnPostAddParameters()
        {
            //В зависимости от типа созданной фигуры преобразовываем интерфейсный объект в конкретный тип
            switch (AddingFigure.FigureType.ToLower())
            {
                case "треугольник":
                    Triangle triangle = AddingFigure as Triangle;
                    triangle.A = double.Parse(Request.Form["SideA"]);
                    triangle.B = double.Parse(Request.Form["SideB"]);
                    triangle.C = double.Parse(Request.Form["SideC"]);
                    break;
                case "прямоугольник":
                    Rectangle rectangle = AddingFigure as Rectangle;
                    rectangle.A = double.Parse(Request.Form["SideA"]);
                    rectangle.B = double.Parse(Request.Form["SideB"]);
                    break;

                default:
                    (AddingFigure as Square).A = double.Parse(Request.Form["SideA"]);
                    break;
            }

            //Задание корректного ID
            AddingFigure.Id = collection.Figures.Max(f => f.Id) + 1;
            Utils.LastFigureId++;

            //Добавление фигуры
            collection.AddFigure(AddingFigure ?? Utils.GetFigure());

            //Вывод в обратном порядке
            figures = collection.OrderByIdDesc();

            FormType = "dropDown";
        }

        #endregion

        //Удаление JSON файла для обновления коллекции 
        public void OnGetDeleteFile()
        {
            collection.DeleteFile();
            figures = collection.Figures;
        }

    }
}
