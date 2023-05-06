using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HomeWork.Controllers;

public class InstructorsController : Controller
{
    // инструкторы
    public IEnumerable<Instructor> Instructors { get; set; } = null!;

    // полное название файла (включая путь)
    public (string RoutesFile, string InstructorsFile) FilesFullName { get; set; } =
        ("App_Data/routes.json", "App_Data/instructors.json");

    #region Конструкторы

    // конструктор по умолчанию
    public InstructorsController()
    {
        // если данные отсутствуют
        if (!LoadData())
        {
            Instructors = Utils.GetDataRoutes(5, 0).instructors;
            SaveData();
        }
    }

    #endregion

    #region Метод

    #region Представления

    // все инструкторы в исходном порядке
    public IActionResult Index()
    {
        ViewBag.Title = "Инструкторы";
        return View(Instructors);
    }


    // вывод сведений об инструкторах в порядке убывания категорий
    public IActionResult OrderByCategoriesDesc()
    {
        ViewBag.PageName = "OrderInstructors";

        ViewBag.Title = "Инструкторы. Сортировка по убыванию категорий";
        return View("Index", Instructors.OrderByDescending(i => i.Category));
    }


    // вывод сведений об инструкторах в алфавитном порядке
    public IActionResult OrderByFullName()
    {
        ViewBag.PageName = "OrderInstructors";
        ViewBag.Title = "Инструкторы. Сортировка в алфавитном порядке";
        return View("Index", Instructors.OrderBy(i => $"{i.Surname}{i.Name}{i.Patronymic}"));
    }


    // вывод сведений об инструкторах с заданной категорией
    public IActionResult SelectByCategory()
    {
        // список категорий
        ViewBag.Categories = new SelectList(Utils.Categories);

        ViewBag.PageName = "SelectInstructors";
        ViewBag.Title = $"Инструкторы. Выборка по категории - \"————\"";
        return View("SelectByCategory", Instructors);
    }

    [HttpPost]
    public IActionResult SelectByCategory(string category)
    {
        ViewBag.Categories = new SelectList(Utils.Categories);

        ViewBag.PageName = "SelectInstructors";
        ViewBag.Title = $"Инструкторы. Выборка по категории - \"{category}\"";
        return View("SelectByCategory", Instructors.Where(i => i.Category == category));
    }

    #endregion

    #region Обработка

    // загрузка данных из JSON
    public bool LoadData()
    {
        // если файла не существует
        if (System.IO.File.Exists(FilesFullName.InstructorsFile))
        {
            Instructors = JsonConvert.DeserializeObject<IEnumerable<Instructor>>(
                              System.IO.File.ReadAllText(FilesFullName.InstructorsFile)) ??
                          new List<Instructor>();

            return true;
        }

        return false;
    }

    // сохранение данных в JSON
    public void SaveData() =>
        System.IO.File.WriteAllText(FilesFullName.InstructorsFile, JsonConvert.SerializeObject(Instructors));

    #endregion

    #endregion
}
