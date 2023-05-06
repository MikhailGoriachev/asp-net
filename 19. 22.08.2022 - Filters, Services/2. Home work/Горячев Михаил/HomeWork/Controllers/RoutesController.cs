using System.Diagnostics.Eventing.Reader;
using System.Net;
using HomeWork.Infrastructure;
using HomeWork.Models;
using HomeWork.Models.ViewModels;
using HomeWork.Models.ViewModels.Routes;
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
        // если модель невалидна
        if (!ModelState.IsValid)
        {
            ViewBag.Points = new SelectList(Utils.Points);
            ViewBag.Instructors = new SelectList(Instructors, "Id", "InstructorToString");
            ViewBag.DifficultyList = new SelectList(Utils.DifficultList);

            ViewBag.IsAdd = true;
            return View("RouteForm", route);
        }

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
        // если модель невалидна
        if (!ModelState.IsValid)
        {
            ViewBag.Points = new SelectList(Utils.Points);
            ViewBag.Instructors = new SelectList(Instructors, "Id", "InstructorToString");
            ViewBag.DifficultyList = new SelectList(Utils.DifficultList);

            ViewBag.IsAdd = false;
            return View("RouteForm", route);
        }

        // поиск редактируемой записи
        var curRoute = Routes?.First(r => r.Id == route.Id);

        // если запись не найдена
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
        return View("SelectByStartPoint",
            new SelectByStartPointViewModel(Routes!.GetViewModel(Instructors!), Utils.Points[0]));
    }

    [HttpPost]
    public IActionResult SelectByStartPoint(SelectByStartPointViewModel model)
    {
        ViewBag.Points = new SelectList(Utils.Points);

        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = $"Маршруты. Выборка по начальному пункту - \"{model.StartPoint}\"";

        model.Routes = Routes!.Where(r => r.StartPoint == model.StartPoint).GetViewModel(Instructors!);
        return View("SelectByStartPoint", model);
    }


    // вывод маршрутов, проходящих через заданный промежуточный пункт
    public IActionResult SelectByMiddlePoint()
    {
        ViewBag.Points = new SelectList(Utils.Points);

        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = "Маршруты. Выборка по промежуточному пункту - \"————\"";
        return View("SelectByMiddlePoint",
            new SelectByMiddlePointViewModel(Routes!.GetViewModel(Instructors!), Utils.Points[0]));
    }

    [HttpPost]
    public IActionResult SelectByMiddlePoint(SelectByMiddlePointViewModel model)
    {
        ViewBag.Points = new SelectList(Utils.Points);

        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = $"Маршруты. Выборка по промежуточному пункту - \"{model.MiddlePoint}\"";

        model.Routes = Routes!.Where(r => r.MiddlePoint == model.MiddlePoint).GetViewModel(Instructors!);

        return View("SelectByMiddlePoint", model);
    }


    // вывод маршрутов с протяженностью, попадающей в заданный интервал
    public IActionResult SelectByRangeLength()
    {
        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title = "Маршруты. Выборка по протяжонности в интервале (км) - \"—————\"";
        return View("SelectByRangeLength", new SelectByRangeLengthViewModel(Routes!.GetViewModel(Instructors!)));
    }

    [HttpPost]
    public IActionResult SelectByRangeLength(SelectByRangeLengthViewModel model)
    {
        ViewBag.PageName = "SelectRoutes";
        ViewBag.Title =
            $"Маршруты. Выборка по протяжонности в интервале (км) - \"{model.MinLength:n0} - {model.MaxLength:n0}\"";

        if (model.MinLength > model.MaxLength)
            ModelState.AddModelError(nameof(model.MaxLength),
                "Неверный диапазон. Максимальное значение не может быть меньше минимального!");

        if (!ModelState.IsValid)
        {
            model.Routes = Routes!.GetViewModel(Instructors!);
            return View("SelectByRangeLength", model);
        }

        model.Routes = Routes!
            .Where(r => r.Length >= model.MinLength && r.Length <= model.MaxLength)
            .GetViewModel(Instructors!);

        return View("SelectByRangeLength", model);
    }


    // получить данные о маршруте по id в формате JSON
    public IActionResult GetRouteData(int id)
    {
        var route = Routes!.FirstOrDefault(r => r.Id == id);

        return Json(route == null
            ? null
            : new RouteViewModel(route,
                Instructors!.First(i => i.Id == route.InstructorId)));
    }


    // метод для проверки обработки исключения
    public IActionResult ExceptionTest() => throw new Exception("RoutesController Error");

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
