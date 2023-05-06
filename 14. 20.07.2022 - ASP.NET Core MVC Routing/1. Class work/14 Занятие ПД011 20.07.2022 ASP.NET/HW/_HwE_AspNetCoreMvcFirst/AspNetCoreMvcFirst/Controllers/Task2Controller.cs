using System.Security.Cryptography;
using System.Text;
using AspNetCoreMvcFirst.Models.Task2;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCoreMvcFirst.Controllers;

public class Task2Controller : Controller
{
    // имя файла данных
    public string FilePath = "App_Data/Enterprise.json";

    // данные о предприятии
    private Enterprise _enterprise { get; set; } = new();

    // в конструкторе создаем файл данных или загружаем коллекцию
    // из файда данных
    public Task2Controller(){
        if (!System.IO.File.Exists(FilePath)) {
            _enterprise.Generate();
            Serialize();
        } else {
            Deserialize();
        } // if
    } // Task2Controller

    // вывод задания на разработку
    // GET /Task2/Problem
    public IActionResult Problem() => View();


    // GET /Epression2/Index вывод коллекции в исходном порядке
    // вывод коллекции в порядке, десериализованном из файла
    public IActionResult Index(){
        ViewBag.Header = $"Данные о предприятии \"{_enterprise.Name}\", прочитанные из файла {Path.GetFileName(FilePath)}";
        return View(_enterprise);
    } // Index


    // GET /Epression2/OrderSurname вывод коллекции с упорядочиванием фамилий по алфавиту 
    public IActionResult OrderSurname() {
        ViewBag.Header = $"Данные о предприятии \"{_enterprise.Name}\", прочитанные из файла {Path.GetFileName(FilePath)}, " +
                         $"работники, упорядочены по алфавиту";
        _enterprise.Workers = _enterprise
            .Workers
            .OrderBy(w => w.Fullname)
            .ToList();

        return View("Index", _enterprise);
    } // OrderSurname


    // GET /Epression2/SalaryDesc вывод коллекции с упорядочиванием по убыванию окладов 
    public IActionResult SalaryDesc(){
        ViewBag.Header = $"Данные о предприятии \"{_enterprise.Name}\", прочитанные из файла {Path.GetFileName(FilePath)}, " +
                         $"по убыванию окладов";
        _enterprise.Workers = _enterprise
            .Workers
            .OrderByDescending(w => w.Salary)
            .ToList();
        return View("Index", _enterprise);
    } // SalaryDesc


    // GET /Epression2/MinSalary отбор работников с окладами, равными минимальному, упорядочивание по алфавиту
    public IActionResult MinSalary(){
        // минимальный оклад
        int minSalary = _enterprise.Workers.Min(w => w.Salary);

        ViewBag.Header = $"Данные о предприятии \"{_enterprise.Name}\", прочитанные из файла {Path.GetFileName(FilePath)}, " +
                         $"работники с окладами, равными минимальному, упорядочены по алфавиту";

        // отбор по минимальному окладу, упорядочивание по алфавиту
        _enterprise.Workers = _enterprise
            .Workers
            .Where(w => w.Salary == minSalary)
            .OrderBy(w => w.Fullname)
            .ToList();
        return View("Index", _enterprise);
    } // MinSalary


    // GET /Epression2/LessAverageExp отбор работников со стажем работы, меньше среднего, упорядочивание по окладу 
    public IActionResult LessAverageExp() {
        // средний стаж работы
        int avgExperience = (int)Math.Ceiling(_enterprise.Workers.Average(w => w.Expirience()));

        ViewBag.Header = $"Данные о предприятии \"{_enterprise.Name}\", прочитанные из файла {Path.GetFileName(FilePath)}, " +
                         $"работники со стажем, меньше среднего {avgExperience}, упорядочены по окладам";

        // отбор по среднему стажу, меньше среднего; упорядочивание по окладу
        _enterprise.Workers = _enterprise.Workers
            .Where(w => w.Expirience() < avgExperience)
            .OrderBy(w => w.Salary)
            .ToList();

        return View("Index", _enterprise);
    } // LessAverageExp

    // ========================================================================

    // Сериализация в JSON
    private void Serialize() {
        string json = JsonConvert.SerializeObject(_enterprise);
        System.IO.File.WriteAllText(FilePath, json, Encoding.UTF8);
    } // Serialize

    // Десериализация из JSON
    private void Deserialize() {
        string json = System.IO.File.ReadAllText(FilePath, Encoding.UTF8);
        _enterprise = JsonConvert.DeserializeObject<Enterprise>(json); ;
    } // Deserialize

} // class Task2Controller
