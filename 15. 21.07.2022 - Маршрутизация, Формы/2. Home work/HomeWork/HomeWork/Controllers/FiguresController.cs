using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeWork.Controllers;

public class FiguresController : Controller
{
    // список фигур
    public IEnumerable<IFigure> Figures { get; set; } = new List<IFigure>();

    // имя файла для сохранения
    public string FileName { get; set; } = "figures.json";

    // коллекция сортировок
    public Dictionary<string, (string Title, Func<IEnumerable<IFigure>> Function)> OrderFuncs { get; set; }

    // коллекция выборок
    public Dictionary<string, (string Title, Func<IEnumerable<IFigure>> Function)> SelectionFuncs { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public FiguresController()
    {
        // загрузка данных
        if (!LoadData())
        {
            Figures = Utils.GetFigurList(Utils.GetInt(10, 15));
            SaveData();
        }

        // сортировки
        OrderFuncs = new()
        {
            // исходная последовательность
            ["default"] = ("Исходная последовательность", () => Figures),

            // упорядочивание по убывание площали
            ["descArea"] = ("Упорядочивание по убыванию площади", () => Figures.OrderByDescending(f => f.Area)),

            // упорядочивание по типу фигуры
            ["type"] = ("Упорядочивание по типу фигур", () => Figures.OrderBy(f => f.Name))
        };

        // выборки
        SelectionFuncs = new()
        {
            // выборка по квадратов и упорядочивание по возрастанию периметра
            ["square"] = ("Квадраты по возрастанию периметра",
                () => Figures.Where(f => f.Name == "Квадрат").OrderBy(f => f.Perimeter)),

            // выборка прямоугольников и упорядочивание по убыванию периметра
            ["rectangle"] = ("Прямоугольники по убыванию периметра",
                () => Figures.Where(f => f.Name == "Прямоугольник").OrderByDescending(f => f.Perimeter)),

            // выбор треугольников и упорядочивание в обратном порядке
            ["triangle"] = ("Треугольники в обратном порядке",
                () => Figures.Where(f => f.Name == "Треугольник").Reverse())
        };
    }

    #endregion

    #region Методы

    #region Представления

    // все фигуры
    public IActionResult Index()
    {
        ViewBag.Title = "Фигуры. Исходные данные";

        return View(Figures);
    }


    // добавление фигуры в коллекцию, при этом тип и размеры фигуры генерируются в коде. Загружать изображение не требуется, формируйте его по типу фигуры
    public IActionResult AddFigure()
    {
        ViewBag.Title = "Фигуры. Исходные данные";

        // добавление случайной фигуры вначало
        Figures = Figures.Prepend(Utils.GetFigure());

        SaveData();

        return View("Index", Figures);
    }


    // редактирование только размеров фигуры в форме на отдельной странице
    public IActionResult EditFigure(int id)
    {
        ViewBag.IsAdd = false;

        IFigure? figure = Figures.FirstOrDefault(f => f.Id == id);

        if (figure == null)
            return NotFound();

        // модель для привязки
        FigureBindingForm? model = default;

        switch (figure.Name)
        {
            case "Квадрат":
                Square square = (figure as Square)!;
                model = new FigureBindingForm(square.Id, square.Name, square.Side, null, null, null);
                break;

            case "Прямоугольник":
                Rectangle rectangle = (figure as Rectangle)!;
                model = new FigureBindingForm(rectangle.Id, rectangle.Name, rectangle.SideA, rectangle.SideB, null,
                    null);
                break;

            case "Треугольник":
                Triangle triangle = (figure as Triangle)!;
                model = new FigureBindingForm(triangle.Id, triangle.Name, triangle.Sides.A, triangle.Sides.B,
                    triangle.Sides.C, null);
                break;
        }

        return View("FigureForm", model);
    }

    [HttpPost]
    public IActionResult EditFigure(FigureBindingForm model)
    {
        // фигура
        IFigure? figure = Figures.FirstOrDefault(f => f.Id == model.Id);

        if (figure == null)
            return NotFound();
        try
        {
            switch (model.Name)
            {
                case "Квадрат":
                    Square square = (figure as Square)!;
                    square.Side = (double)model.A!;
                    break;

                case "Прямоугольник":
                    Rectangle rectangle = (figure as Rectangle)!;
                    rectangle.SideA = (double)model.A!;
                    rectangle.SideB = (double)model.B!;
                    break;

                case "Треугольник":
                    Triangle triangle = (figure as Triangle)!;
                    triangle.Sides = ((double)model.A!, (double)model.B!, (double)model.C!);
                    break;
            }
        }
        catch (Exception e)
        {
            ViewBag.IsAdd = false;
            return View("FigureForm", model with { ErrorMessage = e.Message });
        }

        SaveData();

        return RedirectToAction("Index");
    }

    // удаление фигуры
    public IActionResult RemoveFigure(int id)
    {
        Figures = Figures.Where(f => f.Id != id);

        SaveData();

        ViewBag.Title = "Фигуры. Исходные данные";

        return View("Index", Figures);
    }


    // упорядочивание по названию сортировки
    public IActionResult OrderBy(string orderName)
    {
        var order = OrderFuncs[orderName];

        ViewBag.Title = order.Title;

        return View("Index", order.Function());
    }


    // выборка по названию выборки
    public IActionResult SelectionBy(string selectionName)
    {
        var selection = SelectionFuncs[selectionName];

        ViewBag.Title = selection.Title;

        return View("Index", selection.Function());
    }

    #endregion

    #region Обработка

    // сохранение коллекции в JSON
    public void SaveData() =>
        System.IO.File.WriteAllText(Path.Combine("App_Data", FileName),
            JsonConvert.SerializeObject(Figures, Formatting.Indented,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects }));


    // загрузка коллекции из JSON
    public bool LoadData()
    {
        string path = Path.Combine("App_Data", FileName);

        bool exist = System.IO.File.Exists(path);

        Figures = exist
            ? JsonConvert.DeserializeObject<List<IFigure>>(System.IO.File.ReadAllText(path),
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects })!
            : Figures;

        return exist;
    }

    #endregion

    #endregion
}

// модель для привязки к полям формы
public record FigureBindingForm(int Id, string Name, double? A, double? B, double? C, string? ErrorMessage);
