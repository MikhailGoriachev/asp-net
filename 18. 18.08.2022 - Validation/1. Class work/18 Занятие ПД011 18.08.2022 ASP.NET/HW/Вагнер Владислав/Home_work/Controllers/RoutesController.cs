using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Home_work.Infrastructure;
using Newtonsoft.Json;
using System.IO;

namespace Home_work.Controllers;

public class RoutesController : Controller
{
    public IEnumerable<Models.Route> RoutesColl
    {
        get;
        set;
    }

    //Путь к файлу
    public string FilePath
    {
        get;
        set;
    }

    private string _instructorsFile;
    private List<Instructor> _instructors;


    //Сортировки 
    public Dictionary<string, (string title, Func<IEnumerable<Models.Route>> func)> SortFuncts { get; set; }

    //Выборки 
    public Dictionary<string, (string title, Func<IEnumerable<Models.Route>> func)> SelectionFuncts { get; set; }


    public RoutesController()
    {
        FilePath = $"{Environment.CurrentDirectory}\\App_Data\\routes.json";
        _instructorsFile = $"{Environment.CurrentDirectory}\\App_Data\\instructors.json";

        //Пытаемся получить инструкторов из файла
        _instructors = Utils.Desiralize<List<Instructor>>(_instructorsFile);

        //Если файл есть - десериализация, в противном случае генерация
        if (System.IO.File.Exists(FilePath))
        {
            Desiralize();
        }
        else
        {
            RoutesColl = Utils.GetRoutes(_instructorsFile);
            Serialize();
        }

        //Задание методов сортировок
        SortFuncts = new()
        {
            ["OrderByLengthDesc"] = ("Сортировка по убыванию протяженности", () => RoutesColl?.OrderByDescending(r => r.Length)),
            ["OrderByLengthAsc"] = ("Сортировка по возрастанию протяженности", () => RoutesColl?.OrderBy(r => r.Length)),


            ["OrderByComplexity"] = ("Сортировка по возрастанию сложности", () => {

                //Веса для символьных значений
                Dictionary<string, int> Weight = new()
                {
                    ["A-"] = 0,
                    ["A"]  = 1,
                    ["A+"] = 2,
                    ["B-"] = 3,
                    ["B"]  = 4,
                    ["B+"] = 5,
                    ["C-"] = 6,
                    ["C"]  = 7,
                    ["C+"] = 8,
                };

                return RoutesColl?.OrderBy(r => Weight[r.Complexity]);
            }),

            ["Default"] = ("Исходная коллекция", () => RoutesColl),
        };

        //Определение делегатов выборок
        SelectionFuncts = new()
        {
            ["SelectByStartPoint"] = ("Выборка по заданной стартовой точке", () => {
                string startingPoint = Request.Form["startPoint"];
                return RoutesColl?.Where(r => r.StartingPoint.ToLower() == startingPoint.ToLower());
            }),

            //Выборка по заданной промежуточной точке
            ["SelectByMiddlePoint"] = ("Выборка по заданной промежуточной точке", () => {
                string middlePoint = Request.Form["middlePoint"];
                return RoutesColl?.Where(r => r.MiddlePoint.ToLower() == middlePoint.ToLower());
            }),

            //Выборка по заданному диапазону протяженности
            ["SelectByLengthRange"] = ("Выборка по заданному диапазону протяженности", () => {

                int minLength = int.Parse(Request.Form["minLength"]);
                int maxLength = int.Parse(Request.Form["maxLength"]);

                return RoutesColl?.Where(r => r.Length >= minLength && r.Length <= maxLength);
            }),

            ["Default"] = ("Исходная коллекция", () => RoutesColl),
        };
    }

    public IActionResult Routes()
    {
        ViewBag.IsDefault = true;
        return View(RoutesColl);
    }


    #region CRUD

    //Добавление
    public IActionResult AddRoute()
    {
        ViewBag.AddingFormMode = true;

        //Добавить различные уровни сложности в список
        ViewBag.Complexity = new SelectList(Utils.Complexity);
        ViewBag.Points = new SelectList(Utils.Points);

        //Если из файла не удалось прочитать коллекциюб тогда используем инструкторов по умолчанию
        ViewBag.Instructors = new SelectList(_instructors.Count <= 0?
            Utils.GetInstructors():_instructors, "Id", "SNP");

        //Корректный Id, если коллекция пуста
        int maxId = RoutesColl.Count() > 0 ? RoutesColl.Max(r => r.Id) : 0;

        //Создание объекиа
        return View("Form", new Models.Route() {Id = maxId+1});
    }

    //Редактирование
    public IActionResult EditRoute(int id)
    {
        Models.Route? route = RoutesColl.FirstOrDefault(b => b.Id == id);

        //Если элемент не найден, то форму не запускаем
        if (route is null)
            return View("Routes", RoutesColl);

        ViewBag.AddingFormMode = false;

        //Добавить различные уровни сложности и инструкторов в список
        ViewBag.Complexity = new SelectList(Utils.Complexity);

        ViewBag.StartingPoint = new SelectList(Utils.Points,route.StartingPoint);
        ViewBag.MiddlePoint = new SelectList(Utils.Points,route.MiddlePoint);
        ViewBag.EndPoint = new SelectList(Utils.Points,route.EndPoint);

        //Если из файла не удалось прочитать коллекциюб тогда используем инструкторов по умолчанию
        ViewBag.Instructors = new SelectList(_instructors.Count <= 0 ?
            Utils.GetInstructors() : _instructors,"Id", "SNP");

        //Форма редактирования объекта
        return View("Form", route);
    }

    //Post-обработчик формы на редактирование и добавление 
    [HttpPost]
    public IActionResult FormSubmit(Models.Route model, bool addingmode)
    {
        int Id =int.Parse(Request.Form["instructorSelect"]);

        //Находим конкретного инструктора
        model.InstructorData = _instructors.Count <=0 ? Utils.GetInstructors().First(i => i.Id == Id) : _instructors.First(i => i.Id == Id);

        if (addingmode) {
            //Происходит приведение к List, потому что ни Append, ни Prepend не отрабатывали
            var List = RoutesColl.ToList();
            List.Add(model ?? new Models.Route());

            RoutesColl = List;
         }
        else {

            Models.Route editRoute = RoutesColl.FirstOrDefault(r => r.Id == model.Id);

            editRoute.StartingPoint = model.StartingPoint;
            editRoute.MiddlePoint = model.MiddlePoint;
            editRoute.EndPoint = model.EndPoint;
            editRoute.Complexity = model.Complexity;
            editRoute.Length = model.Length;
            editRoute.InstructorData = model.InstructorData;
        }

        Serialize();

        ViewBag.IsDefault = true;

        //Если добавление - сортируем коллекцию по Id
        return View("Routes", addingmode? RoutesColl.OrderByDescending(b => b.Id) : RoutesColl);
    }

    //Удаление
    public IActionResult DeleteRoute(int id)
    {
        if (RoutesColl.Count() <= 0)
            return View("Routes", RoutesColl);

        //Если есть элемент с определённым идентификатором, тогда удаляем
        if (RoutesColl.FirstOrDefault(b => b.Id == id) != null)
            RoutesColl = RoutesColl.Where(r => r.Id != id);

        Serialize();

        return View("Routes", RoutesColl);
    }
    #endregion

    #region Выборки и сортировки

    //Сортировки

    public IActionResult OrderBy(string value)
    {
        (string header, Func<IEnumerable<Models.Route>> function) tuple = SortFuncts.ContainsKey(value) ? SortFuncts[value] : SortFuncts["Default"];

        ViewBag.Header = tuple.header;

        return View("Routes", tuple.function());
    }

    //Выборки
    [HttpPost]
    public IActionResult Selection(string value)
    {
        (string header, Func<IEnumerable<Models.Route>> function) tuple = SelectionFuncts.ContainsKey(value) ? SelectionFuncts[value] : SelectionFuncts["Default"];

        ViewBag.Header = tuple.header;
        ViewBag.ShowControls = false;

        return View("Routes", tuple.function());
    }

    #endregion

    #region JSON

    public IActionResult DeleteFile()
    {
        if (System.IO.File.Exists(FilePath))
            System.IO.File.Delete(FilePath);

        return View("Routes", RoutesColl);
    }

    //Сериализация 
    public void Serialize()
    {
        string json = JsonConvert.SerializeObject(RoutesColl, Formatting.Indented);

        //Получение имени папки
        string directory = FilePath.Replace(FilePath.Substring(FilePath.LastIndexOf('\\')), "");

        //Есил каталога нет, то создать 
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        System.IO.File.WriteAllText(FilePath, json);
    }

    //Десериализация
    public void Desiralize()
    {
        string json = System.IO.File.ReadAllText(FilePath);

        //Если десериализация прошла не успешно тогда просто сгенерировать новую коллекцию
        RoutesColl = JsonConvert.DeserializeObject<List<Models.Route>>(json) ?? Utils.GetRoutes();
    }

    #endregion
}
