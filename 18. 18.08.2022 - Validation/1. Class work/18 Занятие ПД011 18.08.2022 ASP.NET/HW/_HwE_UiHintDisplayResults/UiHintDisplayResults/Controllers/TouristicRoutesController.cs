using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

using UiHintDisplayResults.Models;
using UiHintDisplayResults.Infrastructure;

namespace UiHintDisplayResults.Controllers;

public class TouristicRoutesController : Controller
{
    // имя папки хранения данных
    private string _folder = "App_Data";

    // коллекция маршрутов
    private List<TouristicRoute> _touristicRoutes;

    // имя файла сведений о маршрутах
    private string _routesFile = "routes.json";

    // коллекция сведений об инструкторах
    private List<Instructor> _instructors;

    // имя файла сведений об инструкторах
    private string _instructorsFile = "instructors.json";

    // коллекция начальных пунктов маршрута
    private SelectList _startPoints;

    // коллекция промежуточных пунктов маршрута
    private SelectList _middlePoints;

    // в конструкторе создаем или десериализуем данные по маршрутам
    // и по инструкторам
    public TouristicRoutesController() {
        // если файла данных нет - создать коллекцию и сохранить
        // ее в файле, если файл есть - загрузить коллекцию из файла
        _routesFile = Path.Combine(_folder, _routesFile);
        _instructorsFile = Path.Combine(_folder, _instructorsFile);

        if (!System.IO.File.Exists(_routesFile) || !System.IO.File.Exists(_instructorsFile)) {
            _touristicRoutes = Utils.InitializeTouristicRoutes();
            _instructors = Utils.InitializeInstructors();
            Serialize();
        } else {
            Deserialize();
        } // if

        // получить коллекции для заполнения списков выбора для начальной
        // и промежуточной точек маршрута
        _startPoints = new SelectList(_touristicRoutes!.Select(tr => tr.StartPoint).Distinct().OrderBy(p => p));
        _middlePoints = new SelectList(_touristicRoutes!.Select(tr => tr.MiddlePoint).Distinct().OrderBy(p => p));
    } // TouristicRoutesController


    // вывод данных о маршрутах
    // GET /Route/Index
    public IActionResult Index() {
        ViewBag.Header = "Маршруты в порядке хранения в файле";

        // передаем коллекцию инструкторов для получения фамилии и инициалов
        // по идентификатору инструктора
        ViewBag.Instructors = _instructors;
        
        // передаем значения для списков выбора форм ввода параметров выборок
        ViewBag.SelectStartPoints = _startPoints;
        ViewBag.SelectMiddlePoints = _middlePoints;
        
        return View(_touristicRoutes);
    } // Index


    // Добавление маршрута - вывод страницы с формой добавления
    // GET /TouristicRoute/AddRoute
    public IActionResult AddRoute() {
        TouristicRoute touristicRoute = new TouristicRoute() { Id = _touristicRoutes.Count > 0 ? _touristicRoutes.Max(b => b.Id) + 1 : 1 };

        // список для выбора; поле значения; поле отображения
        SelectList selectList = new SelectList(
            _instructors.OrderBy(instructor => instructor.ShortName), "Id", "ShortName", _instructors[0]);
        ViewBag.SelectInstructors = selectList;

        // переход на страницу корректировки данных маршрута
        return View("UpdateById", touristicRoute);
    } // AddRoute


    // Удаление сведений о маршруте по Id
    public IActionResult DeleteById(int id) {
        // удаление маршрута из коллекции
        _touristicRoutes.Remove(_touristicRoutes.First(b => b.Id == id));
        Serialize();

        ViewBag.Header = "Маршруты в порядке хранения в файле";

        // передаем коллекцию инструкторов для получения фамилии и инициалов
        // по идентификатору инструктора
        ViewBag.Instructors = _instructors;

        // передаем значения для списков выбора форм ввода параметров выборок
        ViewBag.SelectStartPoints = _startPoints;
        ViewBag.SelectMiddlePoints = _middlePoints;

        return View("Index", _touristicRoutes);
    } // DeleteById


    // Вывод страницы с формой редактирования маршрута
    public IActionResult UpdateById(int id) {
        TouristicRoute touristicRoute = _touristicRoutes.First(b => b.Id == id);

        // список для выбора; поле значения; поле отображения
        ViewBag.SelectInstructors = new SelectList(_instructors, "Id", "ShortName", _instructors[0]);

        // передаем значения для списков выбора начального и промежуточного
        // пунктов маршрута
        ViewBag.SelectStartPoints = _startPoints;
        ViewBag.SelectMiddlePoints = _middlePoints;

        ViewBag.Header = "Маршруты в порядке хранения в файле";
        return View(touristicRoute);
    } // UpdateById


    // обработчик сведений, полученных из формы редактирования маршрутов
    [HttpPost]
    public IActionResult Update(TouristicRoute trData) {
        int index = _touristicRoutes.FindIndex(tr => tr.Id == trData.Id);

        // если индекса нет в коллекции, это добавляемый туристический маршрут 
        if (index < 0) {
            _touristicRoutes.Insert(0, trData);
        } else {
            _touristicRoutes[index] = trData;
        } // if
        Serialize();

        ViewBag.Instructors = _instructors;

        // передаем значения для списков выбора начального и промежуточного
        // пунктов маршрута
        ViewBag.SelectStartPoints = _startPoints;
        ViewBag.SelectMiddlePoints = _middlePoints;

        return View("Index", _touristicRoutes);
    } // Update


    // выдача сведений о маршрутах, сведения упорядочены по заданию
    public IActionResult OrderBy(string key) {
        // коллекция для отсортированных записей, помню, что ее
        // начальное значение null
        IEnumerable<TouristicRoute> orderedRoutes = null!;

        switch (key) {
            // По убыванию протяженности маршрута
            case "length-descend":
                orderedRoutes = _touristicRoutes.OrderByDescending(tr => tr.Length);
                ViewBag.Header = "Маршруты по убыванию протяженности";
                break;

            // По возрастанию протяженности маршрута
            case "length-ascend":
                orderedRoutes = _touristicRoutes.OrderBy(tr => tr.Length);
                ViewBag.Header = "Маршруты, упорядоченные по возрастанию протяженности";
                break;

            // По возрастанию сложности
            case "complexity-ascend":
                orderedRoutes = _touristicRoutes.OrderBy(tr => tr.ComplexitiLevel);
                ViewBag.Header = "Маршруты, упорядоченные по возрастанию сложности";
                break;

            // для успокоения компилятора
            // default:
            //     orderedRoutes = _touristicRoutes.OrderBy(tr => tr.StartPoint);
            //     ViewBag.Header = "Маршруты, упорядоченные по конечной точке";
            //     break;
        } // switch

        ViewBag.Instructors = _instructors;

        // передаем значения для списков выбора начального и промежуточного
        // пунктов маршрута
        ViewBag.SelectStartPoints = _startPoints;
        ViewBag.SelectMiddlePoints = _middlePoints;

        return View("Index", orderedRoutes);
    } // OrderBy


    // Выборка сведений по маршрутам, организуемым туристической фирмой
    // парамеиры выборки передаются через параметры форм
    [HttpPost]
    public IActionResult Where(string key, string startPoint, string middlePoint, int fromLength, int toLength) {
        // коллекция для выбранных данных, понимю, что ее
        // начальное значение null
        IEnumerable<TouristicRoute> selectTouristicRoutes = null!;

        switch (key) {
            // Маршруты с заданным начальным пунктом 
            case "start-point":
                selectTouristicRoutes = _touristicRoutes.Where(tr => tr.StartPoint == startPoint);
                break;

            // Маршруты с заданным промежуточным пунктом 
            case "middle-point":
                selectTouristicRoutes = _touristicRoutes.Where(tr => tr.MiddlePoint == middlePoint);
                break;

            // Маршруты с протяженностью в заданном интервале 
            case "length-in-range":
                selectTouristicRoutes = _touristicRoutes.Where(tr => fromLength <= tr.Length && tr.Length <= toLength);
                break;

            // для успокоения компилятора выбираем маршруты высшей
            // категории сложности
            // default:
            //     selectTouristicRoutes = _touristicRoutes.Where(tr => tr.Complexity == "C+");
            //     break;
        } // switch

        ViewBag.Instructors = _instructors;

        // передаем значения для списков выбора начального и промежуточного
        // пунктов маршрута
        ViewBag.SelectStartPoints = _startPoints;
        ViewBag.SelectMiddlePoints = _middlePoints;

        return View("Index", selectTouristicRoutes);
    } // Where

    // ========================================================================

    // Сериализация в JSON коллекций сведений о туристических маршрутах и об инструкторах
    private void Serialize() {
        string json = JsonConvert.SerializeObject(_touristicRoutes, Formatting.Indented);
        System.IO.File.WriteAllText(_routesFile, json, Encoding.UTF8);

        json = JsonConvert.SerializeObject(_instructors, Formatting.Indented);
        System.IO.File.WriteAllText(_instructorsFile, json, Encoding.UTF8);
    } // Serialize

    // Десериализация из JSON коллекций сведений о туристических маршрутах и об инструкторах
    private void Deserialize() {
        string json = System.IO.File.ReadAllText(_routesFile, Encoding.UTF8);
        _touristicRoutes = JsonConvert.DeserializeObject<List<TouristicRoute>>(json)!;

        json = System.IO.File.ReadAllText(_instructorsFile, Encoding.UTF8);
        _instructors = JsonConvert.DeserializeObject<List<Instructor>>(json)!;
    } // Deserialize

} // class TouristicRoutesController

