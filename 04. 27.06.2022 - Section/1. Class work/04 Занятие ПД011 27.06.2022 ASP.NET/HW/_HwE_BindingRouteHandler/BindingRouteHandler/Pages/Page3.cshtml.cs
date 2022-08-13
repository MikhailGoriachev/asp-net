using BindingRouteHandler.Infrastructure;
using BindingRouteHandler.Models.Task03;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingRouteHandler.Pages
{
    [IgnoreAntiforgeryToken]
    public class Page3Model : PageModel
    {
        // модель хранилища данных, персистентного (persistent) хранилища
        private /* static */ List<IFigure> _figures = new List<IFigure>(new IFigure []
        {
            new Square(8),
            new Square(7),
            new Rectangle(5, 6),
            new Rectangle(9, 12),
            new Triangle(12, 18, 20),
            new Triangle(5, 11, 9),
            new Square(10),
            new Rectangle(7, 10),
            new Triangle(8, 10, 8)
        });

        // свойство для PageModel
        public IEnumerable<IFigure> Figures = new List<IFigure>();

        // обработчик GET-запроса к странице
        public void OnGet() {
            // Поместить данные в переменную для вывода на страницу
            Figures =  _figures;
        } // OnGet


        // Вывод только квадратов, упорядоченных по убыванию площади
        public void OnGetSquaresOnly() {
            // сформировать коллекцию для отображения, с заданной обработкой
            Figures = _figures
                .Where(f => f.Type == "квадрат")
                .OrderByDescending(f => f.Area());
        } // OnGetSquaresOnly


        // Вывод только прямоугольников, упорядоченных по возрастанию периметров
        public void OnGetRectanglesOnly() {
            // сформировать коллекцию для отображения, с заданной обработкой
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "прямоугольник")
                .OrderBy(f => f.Perimeter());
        } // OnGetRectanglesOnly


        // Вывод только треугольников, в обратном порядке, по отношению к порядку в коллекции
        public void OnGetTrianglesOnly() {
            // сформировать коллекцию для отображения, с заданной обработкой
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "треугольник")
                .Reverse();
        } // OnGetTrianglesOnly


        // Вывод коллекции по убыванию площади
        public void OnGetOrderByAreaDesc() {
            // сформировать коллекцию для отображения, с заданной обработкой
            // Figures.Clear();
            Figures = _figures.OrderByDescending(f => f.Area());
        } // OnGetOrderByAreaDesc


        // обработчик POST-запроса к странице
        public void OnPost() {
            // сформировать элемент для добавления в коллекцию
            // код типа элемента
            int type = Utils.GetRandom(0, 2);

            // собственно создание фигуры
            IFigure figure = type switch {
                1 => new Square(Utils.GetRandom(20, 50)),
                2 => new Rectangle(Utils.GetRandom(20, 50), Utils.GetRandom(20, 50)),
                _ => new Triangle()
            };
            _figures.Insert(0, figure);

            // сформировать коллекцию для отображения, с добавленным элементом
            Figures = _figures;
        } // OnPost
    } // class Page3Model 
}
