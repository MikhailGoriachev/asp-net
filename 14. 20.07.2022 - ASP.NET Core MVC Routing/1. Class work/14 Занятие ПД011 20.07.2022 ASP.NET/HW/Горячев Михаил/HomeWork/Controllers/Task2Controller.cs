using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HomeWork.Infrastructure;

namespace HomeWork.Controllers;

// •	вывод коллекции в исходном порядке /Task2/Index;
// •	вывод коллекции с упорядочиванием фамилий по алфавиту /Task2/OrderSurname;
// •	вывод коллекции с упорядочиванием по убыванию окладов /Task2/SalaryDesc;
// •	отбор работников с окладами, равными минимальному, упорядочивание по алфавиту /Task2/MinSalary;
// •	отбор работников со стажем работы, меньше среднего, упорядочивание по окладу /Task2/LessAverageExp.

// Контроллер задания 2
public class Task2Controller : Controller
{
    // исходная коллекция работников
    public List<Worker> Workers { get; set; } = null!;

    // имя файла для сохранения данных
    public string FileName { get; set; }

    // ссылка на окружение
    public IHostEnvironment Environment { get; private set; }

    #region Конструкторы

    // конструктор инициализирующий
    public Task2Controller(IHostEnvironment environment)
    {
        Environment = environment;
        FileName = "workers.json";

        // загрузка данных
        if (!LoadData())
        {
            Workers = new[]
            {
                new Worker("Назаров А. Т.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_001.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Степанов Д. Д.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_002.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Лаврентьев Р. Д.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_003.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Лебедев А. Д.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_004.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Воробьев Ф. В.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_005.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Львов Д. И.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_006.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Абрамов Л. Е.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_007.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Михайлов М. Д.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_008.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Снегирев Д. А.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_009.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Александров Д. Я.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "male_010.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Беляева В. С.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_001.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Степанова Д. Г.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_002.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Никифорова Е. А.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_003.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Исакова Е. А.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_004.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Софронова А. М.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_005.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Ширяева С. А.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_006.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Сафонова К. А.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_007.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Голубева А. П.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_008.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Пастухова К. Г.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_009.jpg", Utils.GetInt(20,26) * 1000),
                new Worker("Николаева М. М.", new DateTime(Utils.GetInt(0, 20) + 2000, Utils.GetInt(1,12), Utils.GetInt(1, 29)), "female_010.jpg", Utils.GetInt(20,26) * 1000),
            }
            .OrderBy(w => Utils.Rand.Next())
            .ToList();

            // сохранение данных
            SaveData();
        }
    }

    #endregion

    #region Методы

    #region Представления

    // исходные данные
    public IActionResult Index()
    {
        ViewBag.Title = "Работники. Исходные данные";

        return View("Workers", Workers);
    }


    // упорядочивание фамилий по алфавиту
    public IActionResult OrderSurname()
    {
        ViewBag.Title = "Работники. Упорядочивание фамилий по алфавиту";

        return View("Workers", Workers.OrderBy(w => w.FullName).ToList());
    }


    // упорядочивание по убыванию окладов
    public IActionResult SalaryDesc()
    {
        ViewBag.Title = "Работники. Упорядочивание по убыванию окладов";

        return View("Workers", Workers.OrderByDescending(w => w.Salary).ToList());
    }


    // выборка по минимальному оклад
    public IActionResult MinSalary()
    {
        // минимальный оклад
        int min = Workers.Min(w => w.Salary);

        ViewBag.Title = $"Работники с минимальным окладом. Минимальный оклад: {min:n2} ₽";

        return View("Workers", Workers.Where(w => w.Salary == min).ToList());
    }


    // выборка со стажем работы меньше среднего
    public IActionResult LessAverageExp()
    {
        // средний стаж
        int avg = (int)Workers.Average(w => w.GetWorkExp());

        ViewBag.Title = $"Работники со стажем работы меньше среднего. Средний стаж (лет): {avg}";

        return View("Workers", Workers.Where(w => w.GetWorkExp() < avg).ToList());
    }

    #endregion


    #region Обработка

    // прочитать данные из файла
    public bool LoadData()
    {
        FileInfo file = new FileInfo(Path.Combine("App_Data", FileName));

        if (file.Exists)
            Workers = JsonConvert.DeserializeObject<List<Worker>>(System.IO.File.ReadAllText(file.FullName)) ??
                      new List<Worker>();

        return file.Exists;
    }


    // сохранить данные в файл
    public void SaveData() =>
        System.IO.File.WriteAllText(Path.Combine("App_Data", FileName), JsonConvert.SerializeObject(Workers));

    #endregion

    #endregion
}
