using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Home_work.Infrastructure;
using Newtonsoft.Json;
using System.IO;
using Home_work.Models.Routes;

namespace Home_work.Controllers;

public class RoutesController : Controller
{
    private IEnumerable<Models.Routes.Route> _routes
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

    //SelectList коллекции для выпадающих списков
    private SelectList _startPointsSelect;
    private SelectList _middlePointsSelect; 


    //Сортировки 
    public Dictionary<string, (string title, Func<IEnumerable<Models.Routes.Route>> func)> SortFuncts { get; set; }

    //Выборки 
    public Dictionary<string, (string title, Func<IEnumerable<Models.Routes.Route>> func)> SelectionFuncts { get; set; }


    public RoutesController()
    {
        FilePath = $"{Environment.CurrentDirectory}\\App_Data\\routes.json";
        _instructorsFile = $"{Environment.CurrentDirectory}\\App_Data\\instructors.json";

        //Пытаемся получить инструкторов из файла
        _instructors = Utils.Deserialize<List<Instructor>>(_instructorsFile);

        //Если файл есть - десериализация, в противном случае генерация
        if (System.IO.File.Exists(FilePath))
        {
            Desiralize();
        }
        else
        {
            _routes = Utils.GetRoutes(_instructorsFile);
            Serialize();
        }

        //Задание методов сортировок
        SortFuncts = new()
        {
            ["OrderByLengthDesc"] = ("Сортировка по убыванию протяженности", () => _routes?.OrderByDescending(r => r.Length)),
            ["OrderByLengthAsc"] = ("Сортировка по возрастанию протяженности", () => _routes?.OrderBy(r => r.Length)),


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

                return _routes?.OrderBy(r => Weight[r.Complexity]);
            }),

            ["Default"] = ("Исходная коллекция", () => _routes),
        };

        //Определение делегатов выборок
        SelectionFuncts = new()
        {
            ["SelectByStartPoint"] = ("Выборка по заданной стартовой точке", () => {
                string startingPoint = Request.Form["startPoint"];
                return _routes?.Where(r => r.StartingPoint.ToLower() == startingPoint.ToLower());
            }),

            //Выборка по заданной промежуточной точке
            ["SelectByMiddlePoint"] = ("Выборка по заданной промежуточной точке", () => {
                string middlePoint = Request.Form["middlePoint"];
                return _routes?.Where(r => r.MiddlePoint.ToLower() == middlePoint.ToLower());
            }),

            //Выборка по заданному диапазону протяженности
            ["SelectByLengthRange"] = ("Выборка по заданному диапазону протяженности", () => {

                int minLength = int.Parse(Request.Form["minLength"]);
                int maxLength = int.Parse(Request.Form["maxLength"]);

                return _routes?.Where(r => r.Length >= minLength && r.Length <= maxLength);
            }),

            ["Default"] = ("Исходная коллекция", () => _routes),
        };

        _startPointsSelect = new SelectList(_routes.Select(r => r.StartingPoint).Distinct());
        _middlePointsSelect = new SelectList(_routes.Select(r => r.MiddlePoint).Distinct());

    }

    public IActionResult Routes()
    {
        ViewBag.IsDefault = true;

        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints =  _startPointsSelect;
        ViewBag.SelectMiddlePoints = _middlePointsSelect;

        return View(_routes);
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
        int maxId = _routes.Count() > 0 ? _routes.Max(r => r.Id) : 0;

        //Создание объекиа
        return View("Form", new Models.Routes.Route() {Id = maxId+1});
    }

    //Редактирование
    public IActionResult EditRoute(int value)
    {
        Models.Routes.Route? route = _routes.FirstOrDefault(b => b.Id == value);

        //Если элемент не найден, то форму не запускаем
        if (route is null)
            return View("Routes", _routes);

        ViewBag.AddingFormMode = false;

        //Добавить различные уровни сложности и инструкторов в список
        ViewBag.Complexity = new SelectList(Utils.Complexity);

        ViewBag.StartingPoint = new SelectList(Utils.Points,route.StartingPoint);
        ViewBag.MiddlePoint = new SelectList(Utils.Points,route.MiddlePoint);
        ViewBag.EndPoint = new SelectList(Utils.Points,route.EndPoint);

        //Если из файла не удалось прочитать коллекцию тогда используем инструкторов по умолчанию
        ViewBag.Instructors = new SelectList(_instructors.Count <= 0 ?
            Utils.GetInstructors() : _instructors, "Id", "SNP", route.InstructorData);

        //Форма редактирования объекта
        return View("Form", route);
    }

    //Post-обработчик формы на редактирование и добавление 
    [HttpPost]
    public IActionResult FormSubmit(Models.Routes.Route model, bool addingmode)
    {
        //Если модель не валидна, тогда запустить представление с формой
        if (!ModelState.IsValid)
            return View("Form", model);

        int Id = int.Parse(Request.Form["instructorSelect"]);

        //Находим конкретного инструктора
        model.InstructorData = _instructors.Count <=0 ? Utils.GetInstructors().First(i => i.Id == Id) : _instructors.First(i => i.Id == Id);

        if (addingmode) {
            //Происходит приведение к List, потому что ни Append, ни Prepend не отрабатывали
            var List = _routes.ToList();
            List.Add(model ?? new Models.Routes.Route());

            _routes = List;
         }
        else {

            Models.Routes.Route editRoute = _routes.FirstOrDefault(r => r.Id == model.Id);

            editRoute.StartingPoint = model.StartingPoint;
            editRoute.MiddlePoint = model.MiddlePoint;
            editRoute.EndPoint = model.EndPoint;
            editRoute.Complexity = model.Complexity;
            editRoute.Length = model.Length;
            editRoute.InstructorData = model.InstructorData;
        }

        Serialize();

        ViewBag.IsDefault = true;

        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints = _startPointsSelect; ;
        ViewBag.SelectMiddlePoints = _middlePointsSelect;

        //Если добавление - сортируем коллекцию по Id
        return View("Routes", addingmode? _routes.OrderByDescending(b => b.Id) : _routes);
    }

    //Удаление
    public IActionResult DeleteRoute(int value)
    {
        if (_routes.Count() <= 0)
            return View("Routes", _routes);

        //Если есть элемент с определённым идентификатором, тогда удаляем
        if (_routes.FirstOrDefault(b => b.Id == value) != null)
            _routes = _routes.Where(r => r.Id != value);

        Serialize();


        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints = _startPointsSelect; ;
        ViewBag.SelectMiddlePoints = _middlePointsSelect;

        return View("Routes", _routes);
    }
    #endregion

    #region Выборки и сортировки

    //Сортировки

    public IActionResult OrderBy(string value)
    {
        (string header, Func<IEnumerable<Models.Routes.Route>> function) tuple = SortFuncts.ContainsKey(value) ? SortFuncts[value] : SortFuncts["Default"];

        ViewBag.Header = tuple.header;

        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints = _startPointsSelect; ;
        ViewBag.SelectMiddlePoints = _middlePointsSelect;

        return View("Routes", tuple.function());
    }

    //Выборки
    [HttpPost]
    public IActionResult Selection(string value)
    {
        (string header, Func<IEnumerable<Models.Routes.Route>> function) tuple = SelectionFuncts.ContainsKey(value) ? SelectionFuncts[value] : SelectionFuncts["Default"];

        ViewBag.Header = tuple.header;
        ViewBag.ShowControls = false;

        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints = _startPointsSelect; ;
        ViewBag.SelectMiddlePoints = _middlePointsSelect;

        return View("Routes", tuple.function());
    }


    public IActionResult SelectByRange ()
    {

        return View("SelectByLength", new LenghtRange());
    }

    [HttpPost]
    public IActionResult SelectByRange (LenghtRange model)
    {
        if (!ModelState.IsValid)
            return View("SelectByLength", model);
        //Валидация на стороне сервера
        else if (model.Min >= model.Max){
            ModelState.AddModelError(nameof(model.Min),"Минимальное значение должно быть < максимального");
            ModelState.AddModelError(nameof(model.Max),"Максимальное значение должно быть > максимального");
            return View("SelectByLength", model);
        }

        //Собственно выборка
        var selected = _routes.Where(r => r.Length >= model.Min && r.Length <= model.Max);


        ViewBag.Header = "Выборка по заданному диапазону протяженности";

        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints = _startPointsSelect; ;
        ViewBag.SelectMiddlePoints = _middlePointsSelect;

        return View("Routes", selected);
    }

    #endregion

    #region JSON

    public IActionResult DeleteFile()
    {
        if (System.IO.File.Exists(FilePath))
            System.IO.File.Delete(FilePath);

        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints = _startPointsSelect; ;
        ViewBag.SelectMiddlePoints = _middlePointsSelect;

        return View("Routes", _routes);
    }

    //Сериализация 
    public void Serialize()
    {
        string json = JsonConvert.SerializeObject(_routes, Formatting.Indented);

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
        _routes = JsonConvert.DeserializeObject<List<Models.Routes.Route>>(json) ?? Utils.GetRoutes();
    }

    #endregion


    //AJAX запрос
    public IActionResult GetRoute(int id)
    {
        Models.Routes.Route route = _routes.FirstOrDefault(r => r.Id == id);

        if (route == null)
            return Json(new Models.Routes.Route());

        return Json(route);
    }

    //Демонстрация работы фильтра исключений
    public IActionResult ExceptionDemo()
    {
        //Выход за переделы массива
        var rout = _routes.ToList()[int.MaxValue];

        return View("Routes", _routes);
    }
}
