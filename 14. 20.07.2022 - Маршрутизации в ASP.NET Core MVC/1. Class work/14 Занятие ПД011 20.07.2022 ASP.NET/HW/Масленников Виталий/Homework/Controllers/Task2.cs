using Homework.Models.Task2;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Homework.Common;

namespace Homework.Controllers;

public class Task2 : Controller
{
    // Путь к файлу данных
    private readonly string _path = "App_Data/workers.json";
    
    // Коллекция с данными работников
    private IEnumerable<Worker> _workers; 

    public Task2()
    {
        if (System.IO.File.Exists(_path))
            _workers = Deserialize();
        else
        {
            _workers = new List<Worker>
            {
                new() {FullName = "Седов И. Т.",     Image = "worker1.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Лавров Е. А.",    Image = "worker2.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Белов Т. Д.",     Image = "worker3.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Агеев Б. И.",     Image = "worker4.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Денисов А. И.",   Image = "worker5.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Шишкин В. Т.",    Image = "worker6.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Матвеев А. И.",   Image = "worker7.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Марков М. А.",    Image = "worker8.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Кулешова Е. Е.",  Image = "worker9.jpg",  Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Яковлева В. Б.",  Image = "worker10.jpg", Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Зайцева М. Д.",   Image = "worker11.jpg", Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Меркулова В. А.", Image = "worker12.jpg", Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Корчагина В. Л.", Image = "worker13.jpg", Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Успенская А. Б.", Image = "worker14.jpg", Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
                new() {FullName = "Петрова А. А.",   Image = "worker15.jpg", Salary = Random.Shared.Next(16, 80) * 500, StartDate = Utils.RandomDate(new DateTime(2010,1,1))}, 
            };
            Serialize(_workers);
        }
    }
    
    // GET /Task2/Index
    public IActionResult Index() => View("Workers", _workers);

    // GET /Task2/OrderSurname
    public IActionResult OrderSurname()
    {
        ViewBag.Title = "Работники, упорядоченные по фамилии";
        return View("Workers", _workers.OrderBy(w => w.FullName));
    }
    
    // GET /Task2/SalaryDesc
    public IActionResult SalaryDesc()
    {
        ViewBag.Title = "Работники, упорядоченные по убыванию окладов";
        return View("Workers", _workers.OrderByDescending(w => w.Salary));
    }
    
    // GET /Task2/MinSalary
    public IActionResult MinSalary()
    {
        var minSalary = _workers.Min(w => w.Salary);
        ViewBag.Title = "Работники с окладом, равным минимальному";
        return View("Workers", _workers.Where(w => w.Salary == minSalary));
    }
    
    // GET /Task2/LessAverageExp
    public IActionResult LessAverageExp()
    {
        var avgYears = _workers.Average(w => w.LengthOfService);
        ViewBag.Title = $"Работники со стажем меньше среднего ({avgYears:0.#})";
        return View("Workers", _workers.Where(w => w.LengthOfService < avgYears));
    }

    private void Serialize(IEnumerable<Worker> workers) =>
        System.IO.File.WriteAllText(_path, JsonConvert.SerializeObject(workers, Formatting.Indented));

    private List<Worker> Deserialize() =>
        JsonConvert.DeserializeObject<List<Worker>>(System.IO.File.ReadAllText(_path))!;
}