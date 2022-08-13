using BootstrapForms.Infrastructure;
using BootstrapForms.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BootstrapForms.Pages
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
        
        // ренедерить ли форму для ввода параметров фигуры
        public bool RenderParamForm { get; private set; } = false;

        // тип фигуры, для которой требуется рендерить форму ввода параметров
        public string FigType { get; private set; } = "none";

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


        // создание фигуры по заданному типу
        public void OnPostCreateFigure(string figType) {
            // чтение коллекции фигур
            Deserialize();

            // собственно создание фигуры
            IFigure figure = figType switch {
                "square" => new Square(1),
                "rectangle" => new Rectangle(1, 1),
                _ => new Triangle()
            };

            // вставим фигуру в начало коллекции, для удобства чтения
            // исохранить коллекцию фигур
            _figures.Insert(0, figure);
            Serialize();

            // сформировать коллекцию для отображения, с добавленным элементом
            Figures = _figures;
            RenderParamForm = true;
            FigType = figType;
        } // OnPostCreateFigure


        // установка параметров для добавленной фигуры
        public void OnPostSetFigParam(string figType, double a, double b, double c) {
            // чтение коллекции фигур
            Deserialize();

            // установить параметры фигуры
            switch (figType) {
                case "triangle":
                    _figures[0] = new Triangle(a, b, c);
                    break;

                case "square":
                    _figures[0] = new Square(a);
                    break;

                default:
                    _figures[0] = new Rectangle(a, b);
                    break;
            } // switch

            // сохранить коллекцию фигур
            Serialize();

            // отображать коллекцию фигур
            RenderParamForm = false;
            Figures = _figures;
        } // OnPostSetFigParam


        // ----------------------------------------------------------------------
        // чтение коллекции из файла в  формате JSON (используем NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _figures = JsonConvert.DeserializeObject<List<IFigure>>(json, new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deserialize

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
