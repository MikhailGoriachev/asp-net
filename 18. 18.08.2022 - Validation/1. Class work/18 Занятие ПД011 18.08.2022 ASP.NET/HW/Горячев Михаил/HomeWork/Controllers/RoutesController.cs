using System.Diagnostics.Eventing.Reader;
using System.Net;
using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Route = HomeWork.Models.Route;

namespace HomeWork.Controllers;

public class RoutesController : Controller
{
    // инструкторы
    public IEnumerable<Instructor>? Instructors { get; set; }

    // маршруты
    public IEnumerable<Route>? Routes { get; set; }

    // полное название файлов с данными (включая путь к нему)
    public (string RoutesFile, string InstructorsFile) FilesFullName { get; set; } =
        ("App_Data/routes.json", "App_Data/instructors.json");

    // коллекция сортировок
    public Dictionary<string, (string Title, Func<IEnumerable<RouteViewModel>> Function)> OrderFuncs { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public RoutesController()
    {
        // если данные отсутствуют
        if (!LoadData())
        {
            (Instructors, Routes) = Utils.GetDataRoutes(5, 20);
            SaveData();
        }

        // сортировки
        OrderFuncs = new()
        {
            // вывод сведений о маршруте по убыванию протяженности маршрута
            ["lengthDesc"] = ("Маршруты. Сортировка по убыванию протяженности маршрута",
                () => Routes!.OrderByDescending(r => r.Length).GetViewModel(Instructors!)),

            // вывод сведений о маршруте по возрастанию протяженности маршрута
            ["lengthAsc"] = ("Маршруты. Сортировка по возрастанию протяженности маршрута",
                () => Routes!.OrderBy(r => r.Length).GetViewModel(Instructors!)),

            // вывод сведений о маршруте по возрастанию сложности
            ["difficultyAsc"] = ("Маршруты. Сортировка по возрастанию сложности",
                () =>
                {
                    var diff = Utils.DifficultyListLevel;

                    return Routes!.OrderBy(r => diff[r.Difficulty]).GetViewModel(Instructors!);
                }),
        };
    }

    #endregion

    #region Методы

    #region Представления

    // все маршруты в исходном порядке
    public IActionResult Index()
    {
        ViewBag.Title = "Маршруты";

        return View(Routes!.Join(Instructors!, r => r.InstructorId, i => i.Id,
            (r, i) => new RouteViewModel(r, i)));
    }


    // добавление маршрута
    public IActionResult AddRoute()
    {
        ViewBag.Points = new SelectList(Utils.Points);
        ViewBag.Instructors = new SelectList(Instructors, "Id", "InstructorToString");
        ViewBag.DifficultyList = new SelectList(Utils.DifficultList);

        ViewBag.IsAdd = true;
        return View("RouteForm", new Route());
    }

    [HttpPost]
    public IActionResult AddRoute(Route route)
    {
        route.Id = ++Route.CurrentMaxId;
        Routes = Routes?.Prepend(route) ?? Routes;

        SaveData();

        ViewBag.Title = "Маршруты";
        return View("Index", Routes!.GetViewModel(Instructors!));
    }


    // редактирование маршрута
    public IActionResult EditRoute(int id)
    {
        Route? route = Routes!.FirstOrDefault(r => r.Id == id);

        // список городов
        ViewBag.Points = new SelectList(Utils.Points);
        ViewBag.Instructors = new SelectList(Instructors, "Id", "InstructorToString");
        ViewBag.DifficultyList = new SelectList(Utils.DifficultList);

        ViewBag.IsAdd = false;
        return route != null ? View("RouteForm", route) : NotFound();
    }

    [HttpPost]
    public IActionResult EditRoute(Route route)
    {
        // поиск редактируемой записи
        var curRoute = Routes?.First(r => r.Id == route.Id);

        // если ничего запись не найдена
        if (curRoute == null) return NotFound();

        var list = Routes!.ToList();

        // замена элемента на новый
        list[list.FindIndex(r => r.Id == route.Id)] = route;

        Routes = list;

        SaveData();

        ViewBag.Title = "Маршруты";
        return View("Index", Routes!.GetViewModel(Instructors!));
    }


    // удаление маршрута
    public IActionResult RemoveRoute(int id)
    {
        // удаление записи
        Routes = Routes!.Where(r => r.Id != id);

        SaveData();

        ViewBag.Title = "Маршруты";
        return View("Index", Routes!.GetViewModel(Instructors!));
    }


    // сортировка по названию обработчика
    public IActionResult OrderBy(string nameHandler)
    {
        var handler = OrderFuncs[nameHandler];

        ViewBag.PageName = "OrderRoutes";
        ViewBag.Title = handler.Title;
        return View("Index", handler.Function());
    }


    // вывод маршрутов с заданным начальным пунктом
    public IActionResult SelectByStartPoint()
    {
        ViewBag.Points = new SelectList(Utils.Points);

        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = "Маршруты. Выборка по начальному пункту - \"————\"";
        return View("SelectByStartPoint", Routes!.GetViewModel(Instructors!));
    }

    [HttpPost]
    public IActionResult SelectByStartPoint(string startPoint)
    {
        ViewBag.Points = new SelectList(Utils.Points);

        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = $"Маршруты. Выборка по начальному пункту - \"{startPoint}\"";
        return View("SelectByStartPoint", Routes!.Where(r => r.StartPoint == startPoint).GetViewModel(Instructors!));
    }


    // вывод маршрутов, проходящих через заданный промежуточный пункт
    public IActionResult SelectByMiddlePoint()
    {
        ViewBag.Points = new SelectList(Utils.Points);

        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = "Маршруты. Выборка по промежуточному пункту - \"————\"";
        return View("SelectByMiddlePoint", Routes!.GetViewModel(Instructors!));
    }

    [HttpPost]
    public IActionResult SelectByMiddlePoint(string middlePoint)
    {
        ViewBag.Points = new SelectList(Utils.Points);

        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = $"Маршруты. Выборка по промежуточному пункту - \"{middlePoint}\"";
        return View("SelectByMiddlePoint", Routes!.Where(r => r.MiddlePoint == middlePoint).GetViewModel(Instructors!));
    }


    // вывод маршрутов с протяженностью, попадающей в заданный интервал
    public IActionResult SelectByRangeLength()
    {
        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = "Маршруты. Выборка по протяжонности в интервале (км) - \"—————\"";
        return View("SelectByRangeLength", Routes!.GetViewModel(Instructors!));
    }

    [HttpPost]
    public IActionResult SelectByRangeLength(int min, int max)
    {
        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = $"Маршруты. Выборка по протяжонности в интервале (км) - \"{min:n0} - {max:n0}\"";
        return View("SelectByRangeLength",
            Routes!.Where(r => r.Length >= min && r.Length <= max).GetViewModel(Instructors!));
    }

    #endregion

    #region Обработка

    // загрузка данных из JSON
    public bool LoadData()
    {
        // если файла не существует
        if (System.IO.File.Exists(FilesFullName.RoutesFile) && System.IO.File.Exists(FilesFullName.InstructorsFile))
        {
            Routes = JsonConvert.DeserializeObject<IEnumerable<Route>>(
                System.IO.File.ReadAllText(FilesFullName.RoutesFile)) ?? new List<Route>();
            Instructors =
                JsonConvert.DeserializeObject<IEnumerable<Instructor>>(
                    System.IO.File.ReadAllText(FilesFullName.InstructorsFile)) ??
                new List<Instructor>();

            return true;
        }

        return false;
    }

    // сохранение данных в JSON
    public void SaveData()
    {
        System.IO.File.WriteAllText(FilesFullName.RoutesFile, JsonConvert.SerializeObject(Routes));
        System.IO.File.WriteAllText(FilesFullName.InstructorsFile, JsonConvert.SerializeObject(Instructors));
    }

    #endregion

    #endregion
}
