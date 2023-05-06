using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UiHintDisplay.Infrastructure;
using UiHintDisplay.Models;

namespace UiHintDisplay.Controllers;

public class InstructorsController : Controller
{
    // имя папки хранения данных
    private string _folder = "App_Data";

    // коллекция сведений об инструкторах
    private List<Instructor> _instructors;

    // имя файла сведений об инструкторах
    private string _instructorsFile = "instructors.json";

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
    } // InstructorsController

    // вывод сведений об инструкторах 
    public IActionResult Index() {
        return View(_instructors);
    }

    // выдача сведений о маршрутах, сведения упорядочены по заданию
    public IActionResult OrderBy(string key) {
        return View("Index", _instructors);
    } // OrderBy


    // Выборка сведений по маршрутам, организуемым туристической фирмой
    public IActionResult Where(string key, string category) {
        return View("Index", _instructors);
    } // Where

    // ========================================================================

    // Сериализация в JSON коллекций сведений о туристических маршрутах и об инструкторах
    private void Serialize() {
        string json = JsonConvert.SerializeObject(_instructors, Formatting.Indented);
        System.IO.File.WriteAllText(_instructorsFile, json, Encoding.UTF8);
    } // Serialize

    // Десериализация из JSON коллекций сведений о туристических маршрутах и об инструкторах
    private void Deserialize() {
        string json = System.IO.File.ReadAllText(_instructorsFile, Encoding.UTF8);
        _instructors = JsonConvert.DeserializeObject<List<Instructor>>(json);
    } // Deserialize

} // class InstructorsController

