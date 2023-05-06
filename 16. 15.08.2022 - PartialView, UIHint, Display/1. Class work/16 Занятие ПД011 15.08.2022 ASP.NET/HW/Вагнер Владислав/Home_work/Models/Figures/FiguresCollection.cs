using System.IO;
using Home_work.Utilities;
using Newtonsoft.Json;

namespace Home_work.Models.Figures
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
            //Если файл есть - десериализация, в противном случае генерация и создание файла
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
        { }
        #endregion

        //Генерация
        private void Generate(int amount)
        {
            Utils.LastFigureId = 1;
            _figures = new List<IFigure>();

            for (int i = 0; i < amount; i++)
                Figures.Add(Utils.GetFigure(_figures.Count > 0? _figures.Max(f => f.Id)+1:1));
                /*Figures.Add(Utils.GetFigure());*/
        }


        #region JSON
        //Сериализация 
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(_figures, Formatting.Indented,
                                                        new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.Objects,
                                                        });

            //Получение пути к папке
            string directory = _filePath.Replace(_filePath.Substring(_filePath.LastIndexOf('\\')), "");

            //Есил каталога нет,тогда создать его
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

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

        #region CRUD
        //Добавление записи
        public void AddFigure()
        {
            //Корректный Id, если коллекция пуста
            int maxId = _figures.Count > 0 ? _figures.Max(f => f.Id):0;

            _figures.Add(Utils.GetFigure(maxId + 1));
            Serialize();
        }

        //Редактирование записи
        public void EditFigure(IFigure figure)
        {
            if (figure == null)
                return;

            IFigure found = _figures.First(f => f.Id == figure.Id);

            //Если фигура найдена, тогда изменяем фигуру в коллекции
            if (found is not null)
                _figures[_figures.IndexOf(found)] = figure;

            Serialize();
        }

        //Удаление фигуры
        public void DeleteFigure(int id)
        {
            if (id <= 0 || id > _figures.Max(f => f.Id))
                return;

            _figures.Remove(_figures?.Find(f => f.Id == id));

            Serialize();
        }

        #endregion

        #region Сортировки

        //Сортировка квадратов по возрастанию периметра
        public IEnumerable<IFigure> OrderSquaresByPerimeter()
            => _figures.Where(f => f.FigureType.ToLower() == "квадрат")
                       .OrderBy(f => f.Perimeter());

        //Сортировка прямоугольников, по убываню периметра
        public IEnumerable<IFigure> OrderRectsByPerimeterDesc()
            => _figures.Where(f => f.FigureType.ToLower() == "прямоугольник")
                       .OrderByDescending(f => f.Perimeter());

        //Сортировка треугольников, в обратном порядке по отношению к порядку в коллекции
        public IEnumerable<IFigure> ReverseTriangles()
            => _figures.Where(f => f.FigureType.ToLower() == "треугольник")
                       .Reverse();

        //Сортировка всей коллекции по убыванию площади
        public IEnumerable<IFigure> OrderByAreaDesc()
            => _figures.OrderByDescending(a => a.Area());

        //Сортировка всей коллекции, упорядоченной по типу фигур
        public IEnumerable<IFigure> OrderByType()
            => _figures.OrderBy(a => a.FigureType);
        #endregion
    }
}
