using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

using UiHintDisplay.Models;
using UiHintDisplay.Infrastructure;

namespace UiHintDisplay.Controllers;

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
    } // TouristicRoutesController


    // вывод данных о маршрутах
    // GET /Route/Index
    public IActionResult Index() {
        // передаем коллекцию инструкторов для получения фамилии и инициалов
        // по идентификатору инструктора
        ViewBag.Instructors = _instructors;
        return View(_touristicRoutes);
    } // Index


    // Добавление маршрута - вывод страницы с формой добавления
    // GET /TouristicRoute/AddRoute
    public IActionResult AddRoute() {
        TouristicRoute touristicRoute = new TouristicRoute() { Id = _touristicRoutes.Count > 0 ? _touristicRoutes.Max(b => b.Id) + 1 : 1 };

        // список для выбора; поле значения; поле отображения
        SelectList selectList = new SelectList(_instructors, "Id", "ShortName", _instructors[0]);
        ViewBag.SelectInstructors = selectList;

        // переход на страницу корректировки данных маршрута
        return View("UpdateById", touristicRoute);
    } // AddRoute


    // Удаление сведений о маршруте по Id
    public IActionResult DeleteById(int id) {
        // удаление маршрута из коллекции
        _touristicRoutes.Remove(_touristicRoutes.First(b => b.Id == id));
        Serialize();

        ViewBag.Instructors = _instructors;
        return View("Index", _touristicRoutes);
    } // DeleteById


    // Вывод страницы с формой редактирования книги
    public IActionResult UpdateById(int id) {
        TouristicRoute touristicRoute = _touristicRoutes.First(b => b.Id == id);

        // список для выбора; поле значения; поле отображения
        SelectList selectList = new SelectList(_instructors, "Id", "ShortName", _instructors[0]);
        ViewBag.SelectInstructors = selectList;

        return View(touristicRoute);
    } // UpdateById


    // обработчик сведений, полученных из формы редактирования книги
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
        return View("Index", _touristicRoutes);
    } // Update


    // выдача сведений о маршрутах, сведения упорядочены по заданию
    public IActionResult OrderBy(string key) {
        ViewBag.Instructors = _instructors;
        return View("Index", _touristicRoutes);
    } // OrderBy


    // Выборка сведений по маршрутам, организуемым туристической фирмой
    public IActionResult Where(string key) {
        ViewBag.Instructors = _instructors;
        return View("Index", _touristicRoutes);
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
        _touristicRoutes = JsonConvert.DeserializeObject<List<TouristicRoute>>(json);

        json = System.IO.File.ReadAllText(_instructorsFile, Encoding.UTF8);
        _instructors = JsonConvert.DeserializeObject<List<Instructor>>(json);
    } // Deserialize

} // class TouristicRoutesController

