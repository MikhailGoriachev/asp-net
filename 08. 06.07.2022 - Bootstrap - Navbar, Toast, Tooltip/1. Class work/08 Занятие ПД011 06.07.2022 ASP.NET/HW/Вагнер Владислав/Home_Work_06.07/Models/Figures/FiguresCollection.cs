using System.IO;
using Home_Work.Utilities;
using Newtonsoft.Json;

namespace Home_Work.Models.Figures
{
    public class FiguresCollection
    {
        //Коллекция фигур
        private List<IFigure> _figures;
        public List<IFigure> Figures
        {
            get
            {
                return _figures;
            }
            private set
            {
                _figures = value ?? new List<IFigure>();
            }
        }

        //Путь к JSON-файлу
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    /*throw new Exception("Строка пути к файлу не может быть пустой!");*/

                    _filePath = "\\App_Data\\figures.json";
                    return;
                }
                _filePath = value;
            }
        }

        #region Конструкторы
        public FiguresCollection(string FilePath)
        {
            this.FilePath = FilePath;
            //Если файл есть - десериализация, в противном случае генерация
            if (File.Exists(_filePath))
            {
                Desiralize();
            }
            else
            {
                Generate(10);
                Serialize();
            }
        }//ctor

        public FiguresCollection() : this($"{Environment.CurrentDirectory}\\App_Data\\figures.json")
        {

        }
        #endregion

        //Генерация
        private void Generate(int amount)
        {
            Utils.LastFigureId = 1;
            _figures = new List<IFigure>();

            for (int i = 0; i < amount; i++)
                Figures.Add(Utils.GetFigure());
        }

        //Добавление записи
        public void AddFigure(IFigure figure)
        {
            _figures.Add(figure ?? Utils.GetFigure());
            Serialize();
        }

        #region JSON
        //Сериализация 
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(_figures,
                                                        new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.Objects,
                                                        });
            File.WriteAllText(_filePath, json);
        }

        //Десериализация
        public void Desiralize()
        {
            
            string json = File.ReadAllText(_filePath);
            Figures = JsonConvert.DeserializeObject<List<IFigure>>(json,
                                                        new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.Objects,
                                                        });

        }

        //Удаление файла
        public void DeleteFile()
        {
            if (!File.Exists(_filePath))
                return;
            File.Delete(_filePath);
        }
        #endregion

        #region Сортировки
        //Сортировка квадратов по убыванию площади
        public IEnumerable<IFigure> OrderSquaresByAreaDesc()
            => _figures.Where(f => f.FigureType.ToLower() == "квадрат")
                       .OrderByDescending(f => f.Area());

        //Сортировка прямоугольников, по возрастанию периметра
        public IEnumerable<IFigure> OrderRectsByPerimeter()
            => _figures.Where(f => f.FigureType.ToLower() == "прямоугольник")
                       .OrderBy(f => f.Perimeter());

        //Сортировка треугольников, в обратном порядке по отношению к порядку в коллекции
        public IEnumerable<IFigure> ReverseTriangles()
            => _figures.Where(f => f.FigureType.ToLower() == "треугольник").Reverse();

        //Сортировка всей коллекции по убыванию площади
        public IEnumerable<IFigure> OrderByAreaDesc()
            => _figures.OrderByDescending(a => a.Area());

        //Сортировка всей коллекции, упорядоченной по типу фигур
        public IEnumerable<IFigure> OrderByType()
            => _figures.OrderBy(a => a.FigureType);

        //Сортировка всей коллекции по убыванию Id
        public IEnumerable<IFigure> OrderByIdDesc()
            => _figures.OrderByDescending(a => a.Id);
        #endregion
    }
}
