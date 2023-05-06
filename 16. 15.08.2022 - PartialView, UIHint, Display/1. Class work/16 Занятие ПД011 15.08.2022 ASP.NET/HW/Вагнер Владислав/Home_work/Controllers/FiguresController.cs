using Microsoft.AspNetCore.Mvc;
using Home_work.Models.Figures;

namespace Home_work.Controllers
{
    public class FiguresController : Controller
    {
        //Класс работы с коллекцией
        private FiguresCollection _figuresCollection;

        //Режим работы представления (в виде карточек или таблица)
        public static bool ShowCards = false;

        public FiguresController()
        {
            _figuresCollection = new();
        }


        public IActionResult Figures()
        {
            //Выводится ли сейчас исходная коллекция
            ViewBag.IsDefault = true;

            ViewBag.ShowCard = ShowCards;
            return View(_figuresCollection.Figures);
        }

        #region CRUD

        //Добавление
        public IActionResult AddFigure()
        {
            //Генерацция фигуры
            _figuresCollection.AddFigure();

            ViewBag.ShowCard = ShowCards;
            return View("Figures",_figuresCollection.Figures.OrderByDescending(f => f.Id));
        }

        //Удаление 
        public IActionResult DeleteFigure(int id)
        {
            //Защита от пустой коллекции
            if (_figuresCollection.Figures.Count <= 0)
                return View("Figures", _figuresCollection.Figures);

            _figuresCollection.DeleteFigure(id);
            ViewBag.ShowCard = ShowCards;

            return View("Figures",_figuresCollection.Figures);
        }

        //Редактирование фигуры
        public IActionResult EditFigure(int id)
        {
            IFigure? figure = _figuresCollection.Figures.FirstOrDefault(f => f.Id == id);

            //Если нужный Id не найден, то возвращаем исходную коллекцию
            if (figure is null)
            {
                ViewBag.IsDefault = true;
                return View("Figures", _figuresCollection.Figures);
            }

            return View("Form",figure);
        }

        [HttpPost]
        //Получение изменённой фигуры из формы
        public IActionResult FormSubmit(/*IFigure figure*/)
        {
            IFigure figure = new Square(1d);

            ViewBag.ShowCard = ShowCards;

            //Поскольку используется интерфейсный тип - передача объекта из формы и форму была 
            //невозможна, поэтому использовался примитивный метод создания объекта.
            switch (Request.Form["type"].ToString().ToLower())
            {
                case "квадрат":
                    figure = new Square(double.Parse(Request.Form["side_a"]));
                    break;

                case "прямоугольник":
                    figure = new Rectangle(double.Parse(Request.Form["side_a"]),
                                           double.Parse(Request.Form["side_b"]));
                    break;
                case "треугольник":
                    figure = new Triangle(double.Parse(Request.Form["side_a"]),
                                          double.Parse(Request.Form["side_b"]),
                                          double.Parse(Request.Form["side_c"]));
                    break;

                default:
                    return View("Figures", _figuresCollection.Figures);
            }

            figure.Id = int.Parse(Request.Form["id"]);

            //Изменяем нужный объект в коллекции
            _figuresCollection.EditFigure(figure);

            return View("Figures", _figuresCollection.Figures);
        }

        #endregion

        #region Сортировки и выборки

        //Квадраты по возрастанию периметра
        public IActionResult SquaresByPerimeter()
        {
            //Не показвать кнопки
            ViewBag.DontShowControls = 1;

            ViewBag.ShowCard = ShowCards;
            return View("Figures", _figuresCollection.OrderSquaresByPerimeter());
        }

        //Прямоугольники по убыванию периметра
        public IActionResult RectsByPerimeterDesc()
        {
            //Не показвать кнопки
            ViewBag.DontShowControls = 1;

            ViewBag.ShowCard = ShowCards;
            return View("Figures", _figuresCollection.OrderRectsByPerimeterDesc());
        }


        //Треугольники в обратномпорядке
        public IActionResult TriangelsReversed()
        {
            //Не показвать кнопки
            ViewBag.DontShowControls = 1;

            ViewBag.ShowCard = ShowCards;
            return View("Figures", _figuresCollection.ReverseTriangles());
        }


        //Коллекция по убыванию площади
        public IActionResult OrderCollectionByArea()
        {
            ViewBag.ShowCard = ShowCards;
            return View("Figures", _figuresCollection.OrderByAreaDesc());
        }

        //Коллекция по убыванию площади
        public IActionResult OrderCollectionByType()
        {
            ViewBag.ShowCard = ShowCards;
            return View("Figures", _figuresCollection.OrderByType());
        }

        #endregion


        //Удаление JSON
        public IActionResult DeleteFile()
        {
            _figuresCollection.DeleteFile();

            ViewBag.ShowCard = ShowCards;
            return View("Figures", _figuresCollection.Figures);
        }


        //Переключение режима вывода
        public IActionResult ShiftView()
        {
            //Поменять свойство режима работы представления
            // ShowCards = ShowCards ? false : true;
            ViewBag.ShowCard =  ShowCards = !ShowCards;

            ViewBag.IsDefault = true;

            return View("Figures", _figuresCollection.Figures);
        }

    }
}
