using System.Data;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using ASP_NET_15_FormAspNet.Infrastructure;

namespace ASP_NET_15_FormAspNet.Models.Figures;

public class FigureList
{
    // коллекця фигур
    public List<IFigure> Figures { get; set; } = new List<IFigure>();

    // имя файла, с данными о коллекции фигур
    private string _fileName; 
    public string FileName {
        get => _fileName;
        set => _fileName = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidDataException("Некорректное имя файла");
    }

    // путь к файлу с данными коллекции фигур
    private string _path = $"{Environment.CurrentDirectory}/App_Data/";
    
    // конструктор по умолчанию 
    public FigureList() : this("Figures.json") {}
    
    // конструктор с парамметрами
    public FigureList(string fileName) {
                 
        // установка значений
        FileName = fileName;
        
        // загрузка данных
        if (!Deserialize())
        {
            Figures = Utilities.GetFigurList(Utilities.GetInt(10, 15));
            Serialize();
        }

    } // FigureList
    
    
    // получить квадраты
    public List<IFigure> Squares => Figures.Where(f => f.Name == "Квадрат").ToList();

    // получить прямоугольники
    public List<IFigure> Rectangles => Figures.Where(f => f.Name == "Прямоугольник").ToList();

    // получить треугольники
    public List<IFigure> Triangles => Figures.Where(f => f.Name == "Треугольник").ToList();
    

    // выборка квадратов, упорядоченных по возростанию периметра
    public List<IFigure> SelectBySquare() => Figures.Where(f => f.Name == "Квадрат")
                                                       .OrderBy(f => f.Perimeter)
                                                       .ToList();

    // выборка прямоугольников, упорядоченных по убыванию периметра
    public List<IFigure> SelectByRectangle() => Figures.Where(f => f.Name == "Прямоугольник")
                                                          .OrderByDescending(f => f.Perimeter)
                                                          .ToList();


    // выборка треугольников, в обратном порядке по отношению к порядку в коллекции
    public List<IFigure> SelectByTriangle() => Figures.Where(f => f.Name == "Треугольник")
                                                         .Reverse()
                                                         .ToList();
    

    // сортировка всей коллекции, упорядоченной по убыванию площади
    public List<IFigure> OrderByDescArea() => Figures.OrderByDescending(f => f.Area)
                                                     .ToList();


    // сортировка всех коллекции, упорядоченной по типу фигур
    public List<IFigure> OrderByName() => Figures.OrderBy(f => f.Name)
                                                 .ToList();
 
    
    // сериализация в JSON
    public void Serialize() =>
        File.WriteAllText(_path + FileName, JsonConvert.SerializeObject(Figures, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects }));


    // десериализация JSON
    public bool Deserialize()
    {
        // если файл отсутствует
        if (!File.Exists(_path + FileName))
            return false;

        Figures = JsonConvert.DeserializeObject<List<IFigure>>(File.ReadAllText(_path + FileName), new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects }) ?? new List<IFigure>();

        return true;
    }

    
}