using H_W6ASP_NET.Infrastructure;
using H_W6ASP_NET.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace H_W6ASP_NET.Pages
{
    public class FiguresModel : PageModel
    {
        // коллекция данных о фигурах
        private List<IFigure> _figures = new List<IFigure>();


        public IEnumerable<IFigure> Figures = new List<IFigure>();

        // полное имя файла для записи/чтения
        private string _path = $"{Environment.CurrentDirectory}/App_Data/figure.json";


        // обработчик GET-запроса к странице
        public void OnGet()
        {
            if (System.IO.File.Exists(_path))
            {
                Deserialize();
            }
            else {
                // формирование коллекции
                _figures = new List<IFigure>(new IFigure[] {
                    new Square(4d),
                    new Square(5d),
                    new Rectangle(2d, 6d),
                    new Rectangle(7d, 12d),
                    new Triangle((2d, 3d, 4d)),
                    new Triangle((4d, 7d, 8d)),
                    new Square(8.3),
                    new Rectangle(5d, 12d),
                    new Rectangle(7.2, 9.5),
                    new Triangle((3d, 4d, 5d))
                });

                // запись в JSON-формате
                Serialize();
            }

            // поместить данные для вывода 
            Figures = _figures;
        } // OnGet


        // вывод только квадратов, по убыванию площади
        public void OnGetSquareAreaDesc()
        {
            Deserialize();
            Figures = _figures
                .Where(f => f.Type == "квадрат")
                .OrderByDescending(f => f.Area());
        } // OnGetSquareAreaDesc


        // вывод только прямоугольников, по возрастанию периметра
        public void OnGetRectanglePerim()
        {
            Deserialize();
            Figures = _figures
                .Where(f => f.Type == "прямоугольник")
                .OrderBy(f => f.Perimeter());
        } // OnGetRectanglePerim

        // вывод только треугольников,
        // в обратном порядке по отношению к порядку в коллекции
        public void OnGetTriangleRevers()
        {
            Deserialize();
            Figures = _figures
                .Where(f => f.Type == "треугольник")
                .Reverse();
        } // OnGetTriangleRevers

        // вывод всей коллекции по убыванию площади
        public void OnGetOrderByAreaDesc() {

            Deserialize();
            Figures = _figures.OrderByDescending(f => f.Area());
        } // OnGetOrderByAreaDesc

        // вывод всей коллекции, упорядоченной по типу фигур
        public void OnGetOrderByType() {
           
            Deserialize();
            Figures = _figures.OrderBy(f => f.Type);
        } // OnGetOrderByType


        // список фигур
        public SelectList TypesFigure { get; } = new SelectList(new List<string>() { "прямоугольник", "треугольник", "квадрат" });


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


        // добавление фигуры
        public void OnPostAddFigure(string type)
        {
            Deserialize();

            // вставка в начало коллекции
            _figures.Insert(0, FigureType switch
            {
                "квадрат" => new Square() { A = SideA },
                "прямоугольник" => new Rectangle() { A = SideA, B = SideB },
                _ => new Triangle() { Sides = (SideA, SideB, SideC) }
            });

            // установка текущей коллекции
            Figures = _figures;

            // сохранение данных
            Serialize();
        }

        // рандомное добавление фигуры
        public void OnGetAddFigureRand()
        {
            Deserialize();

            // сформировать элемент для добавления в коллекцию
            // код типа элемента
            int type = Utils.GetRandom(0, 2);

            // собственно создание фигуры
            IFigure figure = type switch
            {
                1 => new Square(Utils.GetRandom(10d, 20d)),
                2 => new Rectangle(Utils.GetRandom(5d, 7d), Utils.GetRandom(12d, 15d)),
                _ => new Triangle()
            };
            _figures.Insert(0, figure);

            // запись в JSON-формате
            Serialize();

            // коллекция с добавленным элементом
            Figures = _figures;
        } // OnGetAddFigure


        #region Запись и чтение коллекции
        // чтение коллекции из файла в формате JSON 
        private void Deserialize()
        {
            string json = System.IO.File.ReadAllText(_path);
            _figures = JsonConvert.DeserializeObject<List<IFigure>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deseizlize

        // запись коллекции в файл в формате JSON 
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(_figures, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
        #endregion

    } // class FiguresModel
}
