using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using BootstrapIntro.Infrastructure;
using BootstrapIntro.Models.Task02;
using Newtonsoft.Json;


namespace BootstrapIntro.Pages
{
    [IgnoreAntiforgeryToken]
    public class Page2Model : PageModel
    {
        // модель хранилища данных, персистентного (persistent) хранилища
        private List<IFigure> _figures = new List<IFigure>();

        // свойство для PageModel
        public IEnumerable<IFigure> Figures = new List<IFigure>();

        // полное имя файла для записи/чтения
        private string _path = $"{Environment.CurrentDirectory}/App_Data/figures.json";

        // обработчик GET-запроса к странице
        public void OnGet() {
            // если файл есть - читаем данные из файла в коллекцию
            // если файла нет - сформировать коллекцию и записать данные
            // в файл
            // чтение из файла в формате JSON
            if (System.IO.File.Exists(_path)) {
               Deserialize();
            } else {
                // формирование коллекции
                _figures = new List<IFigure>(new IFigure[] {
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

                // запись в JSON-формате
                Serialize();
            } // if

            // Поместить данные в переменную для вывода на страницу
            // установить признак активной кнопки на странице
            Figures =  _figures;
            ViewData["Mode"] = "source";
        } // OnGet


        // Вывод только квадратов, упорядоченных по убыванию площади
        public void OnGetSquaresOnly() {
            // прочитать данные из файла
            Deserialize();

            // сформировать коллекцию для отображения, с заданной обработкой
            Figures = _figures
                .Where(f => f.Type == "квадрат")
                .OrderByDescending(f => f.Area());
            
            // для выделения стилем кнопки текущего режима обработки
            ViewData["Mode"] = "squares";
        } // OnGetSquaresOnly


        // Вывод только прямоугольников, упорядоченных по возрастанию периметров
        public void OnGetRectanglesOnly() {
            // прочитать данные из файла
            Deserialize();

            // сформировать коллекцию для отображения, с заданной обработкой
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "прямоугольник")
                .OrderBy(f => f.Perimeter());

            // для выделения стилем кнопки текущего режима обработки
            ViewData["Mode"] = "rectangles";
        } // OnGetRectanglesOnly


        // Вывод только треугольников, в обратном порядке, по отношению к порядку в коллекции
        public void OnGetTrianglesOnly() {
            // прочитать данные из файла
            Deserialize();

            // сформировать коллекцию для отображения, с заданной обработкой
            // Figures.Clear();
            Figures = _figures
                .Where(f => f.Type == "треугольник")
                .Reverse();

            // для выделения стилем кнопки текущего режима обработки
            ViewData["Mode"] = "triangles";
        } // OnGetTrianglesOnly


        // Вывод коллекции по убыванию площади
        public void OnGetOrderByAreaDesc() {
            // прочитать данные из файла
            Deserialize();

            // сформировать коллекцию для отображения, с заданной обработкой
            Figures = _figures.OrderByDescending(f => f.Area());

            // для выделения стилем кнопки текущего режима обработки
            ViewData["Mode"] = "areas";
        } // OnGetOrderByAreaDesc


        // Вывод всей коллекции, упорядоченной по типу фигур
        public void OnGetOrderByFigType() {
            // прочитать данные из файла
            Deserialize();

            // сформировать коллекцию для отображения, с заданной обработкой
            Figures = _figures.OrderBy(f => f.Type);

            // для выделения стилем кнопки текущего режима обработки
            ViewData["Mode"] = "types";
        } // OnGetOrderByFigType

        // создание фигуры по заданию
        public void OnGetCreateFigure() {
            // чтение коллекции фигур
            Deserialize();

            // сформировать элемент для добавления в коллекцию
            // код типа элемента
            int type = Utils.GetRandom(0, 2);

            // собственно создание фигуры
            IFigure figure = type switch {
                1 => new Square(Utils.GetRandom(20, 50)),
                2 => new Rectangle(Utils.GetRandom(20, 50), Utils.GetRandom(20, 50)),
                _ => new Triangle()
            };

            // вставим фигуру в начало коллекци, для удобства чтения
            _figures.Insert(0, figure);
            Serialize();

            // сформировать коллекцию для отображения, с добавленным элементом
            Figures = _figures;
        } // OnGetCreateFigure


        // ----------------------------------------------------------------------
        // чтение коллекции из файла в  формате JSON (используем NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _figures = JsonConvert.DeserializeObject<List<IFigure>>(json, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deseizlize

        // запись коллекции в файл в формате JSON (используем NewtonSoft)
        public void Serialize() {
            string json = JsonConvert.SerializeObject(_figures, Formatting.Indented,
                new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
    } // class Page3Model 
}
