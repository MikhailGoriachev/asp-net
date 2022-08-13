using HomeWork.Infrastructure;
using Newtonsoft.Json;

namespace HomeWork.Models
{
    // Класс Хранилище фигур
    public class FigureList
    {
        // коллекция фигур
        public List<IFigure> Figures { get; set; } = new List<IFigure>();

        // название файла для сериализации
        public string FileName { get; set; }

        // путь к файлу
        private string _path = $"{Environment.CurrentDirectory}/App_Data/";

        // получить квадраты
        public List<IFigure> Squares => Figures.Where(f => f.Name == "Квадрат").ToList();

        // получить прямоугольники
        public List<IFigure> Rectangles => Figures.Where(f => f.Name == "Прямоугольник").ToList();

        // получить треугольники
        public List<IFigure> Triangles => Figures.Where(f => f.Name == "Треугольник").ToList();

        #region Конструкторы

        // конструктор по умолчанию
        public FigureList()
        {
            // установка значений
            FileName = "figures.json";

            // загрузка данных
            if (!Deserialization())
            {
                Figures = Utils.GetFigurList(Utils.GetInt(10, 15));
                Serialization();
            }
        }


        // конструктор инициализирующий
        public FigureList(List<IFigure> figures, string fileName)
        {
            // установка значений
            Figures = figures;
            FileName = fileName;

            // сохранение данных
            Serialization();
        }

        #endregion

        #region Методы

        // выборка квадратов, сортировка по убыванию площади
        public List<IFigure> SelectionBySquare() => Figures.Where(f => f.Name == "Квадрат")
                                                           .OrderByDescending(f => f.Area)
                                                           .ToList();

        // выборка прямоугольников, сортировка по возрастанию периметра
        public List<IFigure> SelectionByRectangle() => Figures.Where(f => f.Name == "Прямоугольник")
                                                              .OrderBy(f => f.Perimeter)
                                                              .ToList();


        // выборка треугольников, в обратном порядке сортировка по отношению к порядку в коллекции
        public List<IFigure> SelectionByTriangle() => Figures.Where(f => f.Name == "Треугольник")
                                                             .Reverse()
                                                             .ToList();


        // сортировка по убыванию площади
        public List<IFigure> OrderByDescArea() => Figures.OrderByDescending(f => f.Area)
                                                         .ToList();


        // сортировка по типу фигур
        public List<IFigure> OrderByName() => Figures.OrderBy(f => f.Name)
                                                     .ToList();


        // сериализация в JSON
        public void Serialization() =>
            File.WriteAllText(_path + FileName, JsonConvert.SerializeObject(Figures, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects }));


        // десериализация JSON
        public bool Deserialization()
        {
            // если файл отсутствует
            if (!File.Exists(_path + FileName))
                return false;

            Figures = JsonConvert.DeserializeObject<List<IFigure>>(File.ReadAllText(_path + FileName), new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects }) ?? new List<IFigure>();

            return true;
        }


        #endregion
    }
}
