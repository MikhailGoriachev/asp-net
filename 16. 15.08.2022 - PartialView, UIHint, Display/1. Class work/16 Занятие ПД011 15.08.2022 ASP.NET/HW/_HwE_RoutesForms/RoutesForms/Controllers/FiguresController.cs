using System.Text;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using RoutesForms.Infrastructure;
using RoutesForms.Models.Task01;
using RoutesForms.Models.Task02;


namespace RoutesForms.Controllers;

public class FiguresController : Controller
{
    // имя файла данных
    public string FilePath = "App_Data/Figures.json";

    // коллекция фигур 
    private List<IFigure> _figures = null!;

    // конструктор вызывается на каждом запросе к контроллеру
    public FiguresController() {
        // если файла данных нет - создать коллекцию и сохранить
        // ее в файле, если файл есть - загрузить коллекцию из файла
        if (!System.IO.File.Exists(FilePath)) {
            _figures = Utils.InitializeFigures();
            Serialize();
        } else {
            Deserialize();
        } // if
    } // FiguresController


    // Вывод списка фигур
    // GET /Figures/Index
    public IActionResult Index() {
        ViewBag.Header = "Фигуры, хранящиеся в коллекции";

        return View(_figures);
    } // Index


    // Добавление фигуры
    // GET /Figures/Add
    public IActionResult AddFigure() {
        // генерация фигуры
        double lo = 1, hi = 10;
        double a, b, c;

        // код типа фигуры, идентификатор фигуры для коллекции
        int type = Utils.GetRandom(0, 2), 
            id = _figures.Max(f => f.Id) + 1;

        // в этой переменной получаем ссылку на фигуру
        IFigure figure;

        // собственно генерация фигуры
        switch (type) {
            // генерация квадрата
            case 0:
                a = Utils.GetRandom(lo, hi);
                figure = new Square(id, a);
                break;

            // генерация прямоугольника
            case 1:
                a = Utils.GetRandom(lo, hi);
                b = Utils.GetRandom(lo, hi);
                figure = new Rectangle(id, a, b);
                break;

            // генерация треугольника
            default:
                do {
                    a = Utils.GetRandom(lo, hi);
                    b = Utils.GetRandom(lo, hi);
                    c = Utils.GetRandom(lo, hi);
                } while (!Triangle.IsTriangle(a, b, c));

                figure = new Triangle(id, a, b, c);
                break;
        } // switch

        // добавление в начало коллекции, чтобы видеть добавленную 
        // фигуры при рендеринге страницы
        _figures.Insert(0, figure);
        Serialize();

        // показать коллекцию вместе с добавленной фигурой
        return View("Index", _figures);
    } // AddFigure


    // Удаление фигуры из коллекции по Id
    public IActionResult DeleteById(int id) {
        // удаление фигуры из коллекции
        _figures.Remove(_figures.First(f => f.Id == id));
        Serialize();

        return View("Index", _figures);
    } // DeleteById


    // Изменение фигуры в коллекции по ее Id - возвращается страница
    // с разметкой формы для редактирования фигуры
    // GET /Figures/UpdatById/23
    public IActionResult UpdateById(int id) {
        var figure = _figures.First(f => f.Id == id);

        // вывод формы, привязка данных
        return View(new FigureDataModel(figure));
    } // UpdateById


    // обработчик формы для редактирования параметров фигуры
    [HttpPost]
    public IActionResult Update(FigureDataModel figureData) {
        int index = _figures.FindIndex(f => f.Id == figureData.Id);

        switch (figureData.Type) {
            case "квадрат":
                ((Square)_figures[index]).A = figureData.A;
                break;            
            
            case "прямоугольник":
                Rectangle rectangle = (Rectangle) _figures[index];
                (rectangle.A, rectangle.B) = (figureData.A, figureData.B);
                break;

            // присваивание значений для треугольника
            default:
                Triangle triangle = (Triangle)_figures[index];
                // по логике присваивания свойству Sides изменения пройдут только для 
                // корректных значений
                triangle.Sides = (figureData.A, figureData.B, figureData.C);
                break;
        } // switch

        // сохранить измененную коллекцию в файле
        Serialize();

        // переход на страницу вывода коллекции
        return View("Index", _figures);
    } // Update


    // Сортировки коллекции фигур по заданию
    public IActionResult OrderBy(string key) {
        IEnumerable<IFigure> result = null!;

        switch (key) {
            // по убыванию площади
            case "area-desc":
                result = _figures
                    .OrderByDescending(f => f.Area());
                ViewBag.Header = "Фигуры, упорядоченные по убыванию площади";
                break;

            // по типу фигуры
            case "type":
                result = _figures
                    .OrderBy(f => f.Type);
                ViewBag.Header = "Фигуры, упорядоченные по типу";
                break;
        } // switch

        // выводим упорядоченную копию
        return View("Index", result);
    } // OrderBy


    // Выборки из коллекции фигур по заднию
    public IActionResult Where(string key) {
        IEnumerable<IFigure> result = null!;

        switch (key) {
            // Квадраты по возрастанию периметра
            case "squares-perims-ascend":
                result = _figures
                    .Where(f => f.Type == "квадрат")
                    .OrderBy(f => f.Perimeter());
                ViewBag.Header = "Квадраты по возрастанию периметра";
                break;

            // Прямоугольники по убыванию периметра
            case "rectangles-perim-descend":
                result = _figures
                    .Where(f => f.Type == "прямоугольник")
                    .OrderByDescending(f => f.Perimeter());
                ViewBag.Header = "Прямоугольники по убыванию периметра";
                break;

            // Треугольники в реверсном порядке хранения
            case "triangles-reverse":
                result = _figures
                    .Where(f => f.Type == "треугольник")
                    .Reverse();
                ViewBag.Header = "Треугольники в порядке, обратном порядку хранения в файле";
                break;
        } // switch

        return View("Index", result);
    } // Where


    // ========================================================================

    // чтение коллекции из файла в формате JSON (используем NewtonSoft)
    private void Deserialize() {
        string json = System.IO.File.ReadAllText(FilePath);
        _figures = JsonConvert.DeserializeObject<List<IFigure>>(json,
            new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
    } // Deserialize

    // запись коллекции в файл в формате JSON (используем NewtonSoft)
    public void Serialize() {
        string json = JsonConvert.SerializeObject(_figures,
            Formatting.Indented,
            new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Objects,
            });
        System.IO.File.WriteAllText(FilePath, json);
    } // Serialize
} // FiguresController
