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
        //перезагрузка страницы и объект
        public IFigure AddingFigure { get; set; } = Utils.FormFigure;


        //Список для выпадающего списка
        public SelectList types = new SelectList(new List<IFigure>());

        //Загрузка списка в форме доавбления
        public void LoadDropDown()
        {
            types = new SelectList(collection.Figures.DistinctBy(f => f.FigureType).Select(f => f.FigureType));
        }

        //Тип формы 
        public string FormType = "dropDown";

        public void OnGet()
        {
            FormType = "dropDown";
            figures = collection.Figures;
            LoadDropDown();
        }

        #region Сортировки
        //Исходная коллекция
        public void OnGetShowDefault()
        {
            figures = collection.Figures;
            LoadDropDown();
        }

        //Сортировка только квадратов
        public void OnGetSortSquaresByArea()
        {
            figures = collection.OrderSquaresByAreaDesc();
            LoadDropDown();
        }

        //Сортировка только прямоугольников
        public void OnGetSortRectanglesByPerimeter()
        {
            figures = collection.OrderRectsByPerimeter();
            LoadDropDown();
        }

        //Треугольники в обратном порядке
        public void OnGetReverseTriangles()
        {
            figures = collection.ReverseTriangles();
            LoadDropDown();
        }

        //Сортировка коллекции по убыванию площади
        public void OnGetSortBySquare()
        {
            figures = collection.OrderByAreaDesc();
            LoadDropDown();
        }

        //Сортировка коллекции по типу
        public void OnGetSortByType()
        {
            figures = collection.OrderByType();
            LoadDropDown();
        }

        #endregion

        #region Добавление
        /*Добавление записи (случайная фигура)*/
        public void OnGetAddFigure()
        {
            collection.AddFigure(Utils.GetFigure());

            //Сортировка в обратном порядке
            figures = collection.Figures.OrderByDescending(f => f.Id);
            LoadDropDown();
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
            LoadDropDown();
            figures = collection.Figures;
        }

        //Добавление параметров фигуры
        public void OnPostAddParemeters()
        {
            //В зависимости от типа созданной фигуры преобразовываем интерфейсный объект в конкретный тип
            switch (AddingFigure.FigureType.ToLower())
            {
                case "треугольник":
                    Triangle triangle = AddingFigure as Triangle;
                    triangle.A = /*int*/double.Parse(Request.Form["SideA"]);
                    triangle.B = /*int*/double.Parse(Request.Form["SideB"]);
                    triangle.C = /*int*/double.Parse(Request.Form["SideC"]);
                    break;
                case "прямоугольник":
                    Rectangle rectangle = AddingFigure as Rectangle;
                    rectangle.A = /*int*/double.Parse(Request.Form["SideA"]);
                    rectangle.B = /*int*/double.Parse(Request.Form["SideB"]);
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

            LoadDropDown();

            FormType = "dropDown";
        }

        #endregion

        //Удаление JSON файла для обновления коллекции 
        public void OnGetDeleteFile()
        {
            collection.DeleteFile();
            figures = collection.Figures;
            LoadDropDown();
        }

    }
}
