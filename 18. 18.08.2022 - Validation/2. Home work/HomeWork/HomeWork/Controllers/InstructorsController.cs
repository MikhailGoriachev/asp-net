using HomeWork.Infrastructure;
using HomeWork.Models;
using HomeWork.Models.ViewModels.Instructors;
using HomeWork.Models.ViewModels.Routes;
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

    // коллекция сортировок
    public Dictionary<string, (string Title, Func<IEnumerable<Instructor>> Function)> OrderFuncs { get; set; }

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

        // сортировки
        OrderFuncs = new()
        {
            // вывод сведений о маршруте по убыванию протяженности маршрута
            ["categoriesDesc"] = ("Инструкторы. Сортировка по убыванию категорий",
                () => Instructors.OrderByDescending(i => i.Category)),

            // вывод сведений о маршруте по убыванию протяженности маршрута
            ["fullName"] = ("Инструкторы. Сортировка в алфавитном порядке",
                () => Instructors.OrderBy(i => $"{i.Surname}{i.Name}{i.Patronymic}")),
        };
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


    // редактирование инструктора
    public IActionResult EditInstructor(int id)
    {
        ViewBag.Categories = new SelectList(Utils.Categories);

        Instructor? instructor = Instructors!.FirstOrDefault(r => r.Id == id);

        ViewBag.IsAdd = false;
        return instructor != null ? View("InstructorForm", instructor) : NotFound();
    }

    [HttpPost]
    public IActionResult EditInstructor(Instructor instructor)
    {
        // если модель невалидна
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(Utils.Categories);
            ViewBag.IsAdd = false;
            return View("InstructorForm", instructor);
        }

        // поиск редактируемой записи
        var curInst = Instructors.FirstOrDefault(r => r.Id == instructor.Id);

        // если ничего запись не найдена
        if (curInst == null) return NotFound();

        var list = Instructors.ToList();

        // замена элемента на новый
        list[list.FindIndex(r => r.Id == instructor.Id)] = instructor;

        Instructors = list;

        SaveData();

        ViewBag.Title = "Маршруты";
        return View("Index", Instructors);
    }


    // сортировка по названию обработчика
    public IActionResult OrderBy(string nameHandler)
    {
        var handler = OrderFuncs[nameHandler];

        ViewBag.PageName = "OrderInstructors";
        ViewBag.Title = handler.Title;
        return View("Index", handler.Function());
    }


    // вывод сведений об инструкторах с заданной категорией
    public IActionResult SelectByCategory()
    {
        // список категорий
        ViewBag.Categories = new SelectList(Utils.Categories);

        ViewBag.PageName = "SelectInstructors";
        ViewBag.Title = "Инструкторы. Выборка по категории - \"————\"";
        return View("SelectByCategory", new SelectByCategoryViewModel(Instructors, Utils.Categories[0]));
    }

    [HttpPost]
    public IActionResult SelectByCategory(SelectByCategoryViewModel model)
    {
        ViewBag.Categories = new SelectList(Utils.Categories);

        ViewBag.PageName = "SelectInstructors";
        ViewBag.Title = $"Инструкторы. Выборка по категории - \"{model.Category}\"";

        model.Instructors = Instructors.Where(i => i.Category == model.Category);

        return View("SelectByCategory", model);
    }


    // получить данные об инструкторе в виде JSON
    public IActionResult GetDataInstructor(int id) => Json(Instructors.FirstOrDefault(i => i.Id == id));

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
