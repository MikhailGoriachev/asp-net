using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UiHintDisplayResults.Infrastructure;
using UiHintDisplayResults.Models;

namespace UiHintDisplayResults.Controllers;

public class InstructorsController : Controller
{
    // имя папки хранения данных
    private string _folder = "App_Data";

    // коллекция сведений об инструкторах
    private List<Instructor> _instructors;

    // имя файла сведений об инструкторах
    private string _instructorsFile = "instructors.json";

    // категории инструкторов
    private SelectList _categories;

    // в конструкторе создаем или десериализуем данные по инструкторам
    public InstructorsController() {
        // если файла данных нет - создать коллекцию и сохранить
        _instructorsFile = Path.Combine(_folder, _instructorsFile);

        if (!System.IO.File.Exists(_instructorsFile)) {
            _instructors = Utils.InitializeInstructors();
            Serialize();
        } else {
            Deserialize();
        } // if

        _categories = new SelectList(new[] { "A", "B", "C" });
    } // InstructorsController


    // вывод сведений об инструкторах 
    public IActionResult Index() {
        ViewBag.Header = "Инструкторы маршрутов в порядке хранения в файле";
        ViewBag.SelectCategories = _categories;
        return View(_instructors);
    }

    // выдача сведений о маршрутах, сведения упорядочены по заданию
    public IActionResult OrderBy(string key) {
        // коллекция для отсортированных сведений, понимаю, что ее
        // начальное значение null
        IEnumerable<Instructor> orderedInstructors = null!;

        switch (key) {
            // В порядке убывания категорий
            case "category-descend":
                orderedInstructors = _instructors.OrderByDescending(instructor => instructor.Category);
                ViewBag.Header = "Инструкторы маршрутов в порядке убывания категорий";
                break;

            // В алфвитном порядке
            case "alphabet":
                orderedInstructors = _instructors
                    .OrderBy(instructor => instructor.Surname)
                    .ThenBy(instructor => instructor.Name)
                    .ThenBy(instructor => instructor.Patronymic);
                ViewBag.Header = "Инструкторы маршрутов в алфавитном порядке";
                break;

                // для успокоения компилятора
                // default:
                //     orderedInstructors = _instructors.OrderBy(instructor => instructor.BornDate);
                //     ViewBag.Header = "Инструкторы маршрутов в порядке убывания возраста";
                //     break;
        } // switch

        // для ипользования в  форме
        ViewBag.SelectCategories = _categories;
        return View("Index", orderedInstructors);
    } // OrderBy


    // Выборка сведений по маршрутам, организуемым туристической фирмой
    [HttpPost]
    public IActionResult Where(string category) {
        // выборка по категории инструктора
        IEnumerable<Instructor> selected = _instructors.Where(instructor => instructor.Category == category);

        ViewBag.Header = $"Инструкторы маршрутов с категорией \"{category}\"";
        ViewBag.SelectCategories = _categories;
        return View("Index", selected);
    } // Where

    // Вывод страницы с формой редактирования сведений об инструкторе
    public IActionResult UpdateById(int id) {
        Instructor instrucor = _instructors.First(instructor => instructor.Id == id);

        // список для выбора категории инструктора
        ViewBag.SelectCategories = _categories;

        ViewBag.Header = "Редактирование сведений об инструкторе";
        return View(instrucor);
    } // UpdateById


    // обработчик сведений, полученных из формы редактирования
    // сведений об инструкторе
    [HttpPost]
    public IActionResult Update(Instructor instructorData) {
        int index = _instructors.FindIndex(instructor => instructor.Id == instructorData.Id);

        // если индекса нет в коллекции, это добавляемый туристический маршрут 
        _instructors[index] = instructorData;
        Serialize();

        // вывести обновленную коллекцию
        ViewBag.Header = "Инструкторы маршрутов в порядке хранения в файле";
        ViewBag.SelectCategories = _categories;
        return View("Index", _instructors);
    } // Update


    // на этот запрос отдаем клиенту JSON
    // GET /Instructors/Details/id
    public IActionResult Details(int key) =>
        Json(_instructors.Find(instructor => instructor.Id == key));
    
    

    // ========================================================================

    // Сериализация в JSON коллекций сведений о туристических маршрутах и об инструкторах
    private void Serialize() {
        string json = JsonConvert.SerializeObject(_instructors, Formatting.Indented);
        System.IO.File.WriteAllText(_instructorsFile, json, Encoding.UTF8);
    } // Serialize

    // Десериализация из JSON коллекций сведений о туристических маршрутах и об инструкторах
    private void Deserialize() {
        string json = System.IO.File.ReadAllText(_instructorsFile, Encoding.UTF8);
        _instructors = JsonConvert.DeserializeObject<List<Instructor>>(json)!;
    } // Deserialize

} // class InstructorsController
