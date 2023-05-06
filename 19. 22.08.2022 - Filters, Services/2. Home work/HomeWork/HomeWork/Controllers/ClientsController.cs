using HomeWork.Infrastructure;
using HomeWork.Models;
using HomeWork.Models.ViewModels.Clients;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HomeWork.Controllers;

public class ClientsController : Controller
{
    // инструкторы
    public IEnumerable<Client> Clients { get; set; } = null!;

    // полное название файла (включая путь)
    public string FileFullName { get; set; } = "App_Data/clients.json";

    // коллекция сортировок
    public Dictionary<string, (string Title, Func<IEnumerable<Client>> Function)> OrderFuncs { get; set; }

    // окружение
    public IHostEnvironment Environment { get; set; }

    // логгер
    public Logger Logger { get; set; }

    #region Конструкторы

    // конструктор по умолчанию
    public ClientsController(IHostEnvironment environment, Logger logger)
    {
        Logger = logger;
        Environment = environment;

        // если данные отсутствуют
        if (!LoadData())
        {
            Clients = Utils.Clients;
            SaveData();
        }

        // сортировки
        OrderFuncs = new()
        {
            // вывод коллекции в порядке хранения в файле данных
            ["default"] = ("Клиенты", () => Clients),

            // вывод коллекции в алфавитном порядке
            ["fullName"] = ("Клиенты. Сортировка по ФИО в алфавитном порядке",
                () => Clients.OrderBy(i => $"{i.Surname}{i.Name}{i.Patronymic}")),

            // вывод коллекции по убыванию возраста
            ["yearsOldDesc"] = ("Клиенты. Сортировка по убыванию возраста",
                () => Clients.OrderByDescending(i => i.YearsOld)),
        };
    }

    #endregion

    #region Метод

    #region Представления

    // все клиенты
    public IActionResult Index()
    {
        var order = OrderFuncs["default"];

        ViewBag.Title = order.Title;
        return View(order.Function());
    }


    // добавить клиента
    public IActionResult AddClient()
    {
        ViewBag.IsAdd = true;
        return View("ClientForm", new ClientBindingModel());
    }

    [HttpPost]
    public async Task<ViewResult> AddClientAsync(ClientBindingModel model)
    {
        // если модель невалидна
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = true;
            return View("ClientForm", model);
        }

        var client = new Client(model.Surname, model.Name, model.Patronymic, model.YearsOld,
            model.TelNumber, model.Mail, "", model.IsActive,
            model.FormFile?.FileName ?? "default-client.jpg");

        Clients = Clients.Prepend(client);


        // загрузка фотографии клиента
        if (model.FormFile != null)
            Utils.SaveFile(@"wwwroot\images\persons", model.FormFile);

        SaveData();

        ViewBag.Client = $"Клиент: {client.Id}. {client.Surname} {client.Name} {client.Patronymic}";
        ViewBag.Title = "Создание пароля";

        // запись в логи
        await Logger.WriteLogAsync(
            $"AddClient: id = {client.Id}; {client.Surname}; {client.Name}; {client.Patronymic}; YearsOld = {client.YearsOld}; {client.Mail}; {client.TelNumber}; IsActive = {client.IsActive}; Password = {Utils.Crypt(client.Password!)}; {client.ImageFile}");

        // задание пароля
        return View("ClientPasswordForm", new ClientPasswordBindingModel(client));
    }


    // редактировать клиента
    public IActionResult EditClient(int id)
    {
        var client = Clients.FirstOrDefault(c => c.Id == id);

        if (client == null)
            return NotFound();

        ViewBag.IsAdd = false;
        return View("ClientForm", new ClientBindingModel(client));
    }


    [HttpPost]
    public async Task<IActionResult> EditClientAsync(ClientBindingModel model)
    {
        // если модель невалидна
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;
            return View("ClientForm", model);
        }

        var client = Clients.FirstOrDefault(c => c.Id == model.Id);

        if (client == null)
            return NotFound();

        client.Surname = model.Surname;
        client.Name = model.Name;
        client.Patronymic = model.Patronymic;
        client.YearsOld = model.YearsOld;
        client.TelNumber = model.TelNumber;
        client.Mail = model.Mail;
        client.IsActive = model.IsActive;
        client.ImageFile = model.FormFile != null ? model.FormFile.FileName : model.ImageFile;

        ViewBag.Title = "Клиенты";

        // загрузка фотографии клиента
        if (model.FormFile != null)
            Utils.SaveFile(Path.Combine("wwwroot", "images", "persons"), model.FormFile);

        SaveData();

        // запись в логи
        await Logger.WriteLogAsync(
            $"EditClient: id = {client.Id}; {client.Surname}; {client.Name}; {client.Patronymic}; YearsOld = {client.YearsOld}; {client.Mail}; {client.TelNumber}; IsActive = {client.IsActive}; Password = {Utils.Crypt(client.Password!)}; {client.ImageFile}");

        return View("Index", Clients);
    }


    // редактировать пароля клиента
    public IActionResult EditClientPassword(int id)
    {
        var client = Clients.FirstOrDefault(c => c.Id == id);

        if (client == null)
            return NotFound();

        ViewBag.Client = $"Клиент: {client.Id}. {client.Surname} {client.Name} {client.Patronymic}";

        ViewBag.Title = "Создание нового пароля";

        return View("ClientPasswordForm", new ClientPasswordBindingModel(client));
    }


    [HttpPost]
    public async Task<IActionResult> EditClientPasswordAsync(ClientPasswordBindingModel model)
    {
        // если модель невалидна
        if (!ModelState.IsValid)
        {
            ViewBag.IsAdd = false;
            return View("ClientPasswordForm", model);
        }

        var client = Clients.FirstOrDefault(c => c.Id == model.Id);

        if (client == null)
            return NotFound();

        client.Password = model.Password;

        ViewBag.Title = "Клиенты";

        SaveData();

        // запись в логи
        await Logger.WriteLogAsync(
            $"EditClient: id = {client.Id}; {client.Surname}; {client.Name}; {client.Patronymic}; YearsOld = {client.YearsOld}; {client.Mail}; {client.TelNumber}; IsActive = {client.IsActive}; Password = {Utils.Crypt(client.Password!)}; {client.ImageFile}");

        return View("Index", Clients);
    }


    // удаление клиента
    public IActionResult RemoveClient(int id)
    {
        var client = Clients.FirstOrDefault(r => r.Id == id);

        if (client == null)
            return NotFound();

        // удаление записи
        Clients = Clients.Where(r => r.Id != id);

        // удаление файла (если изображение не является стандартным)
        if (client.ImageFile != "default-client.jpg")
            System.IO.File.Delete(Path.Combine(Environment.ContentRootPath, "wwwroot", "images", "persons",
                client.ImageFile!));

        SaveData();

        ViewBag.Title = "Клиенты";
        return View("Index", Clients);
    }


    // сортировка
    public IActionResult OrderBy(string nameHandler)
    {
        ViewBag.PageName = "OrderClients";

        var order = OrderFuncs[nameHandler];

        ViewBag.Title = order.Title;
        return View("OrderBy", order.Function());
    }

    // вывод сведений о клиентах с заданным диапазоном возраста (требуется валидация параметров на стороне клиента и на стороне сервера)
    public IActionResult SelectByRangeYearsOld()
    {
        ViewBag.PageName = "SelectClients";
        ViewBag.Title = "Клиенты. Выборка по возрасту в интервале (лет) - \"————\"";
        return View("SelectByRangeYearsOld", new SelectByRangeYearsOldViewModel(Clients));
    }

    [HttpPost]
    public IActionResult SelectByRangeYearsOld(SelectByRangeYearsOldViewModel model)
    {
        ViewBag.PageName = "SelectClients";
        ViewBag.Title =
            $"Клиенты. Выборка по возрасту в интервале (лет) - \"{model.MinYears} - {model.MaxYears}\"";

        if (model.MinYears > model.MaxYears)
            ModelState.AddModelError(nameof(model.MaxYears),
                "Неверный диапазон. Максимальное значение не может быть меньше минимального!");

        if (!ModelState.IsValid)
        {
            model.Clients = Clients;
            return View("SelectByRangeYearsOld", model);
        }

        model.Clients = Clients.Where(c => c.YearsOld >= model.MinYears && c.YearsOld <= model.MaxYears);

        return View("SelectByRangeYearsOld", model);
    }


    // вывод сведений о клиентах по фамилии
    public IActionResult SelectBySurname()
    {
        ViewBag.SurnameList = new SelectList(Clients.Select(c => c.Surname));
        ViewBag.PageName = "SelectClients";
        ViewBag.Title = "Клиенты. Выборка по фамилии - \"—————\"";
        return View("SelectBySurname", new SelectBySurnameViewModel(Clients));
    }

    [HttpPost]
    public IActionResult SelectBySurname(SelectBySurnameViewModel model)
    {
        ViewBag.SurnameList = new SelectList(Clients.Select(c => c.Surname));
        ViewBag.PageName = "SelectClients";
        ViewBag.Title = $"Клиенты. Выборка по фамилии - \"{model.Surname}\"";

        if (!ModelState.IsValid)
            return View("SelectBySurname", model);

        model.Clients = Clients.Where(c => c.Surname == model.Surname);

        return View("SelectBySurname", model);
    }


    // вывод сведений о постоянных клиентах
    public IActionResult SelectByActive()
    {
        ViewBag.SurnameList = new SelectList(Clients.Select(c => c.Surname));
        ViewBag.PageName = "SelectClients";
        ViewBag.Title = "Клиенты. Выборка по постоянных клиентов";
        return View("SelectByActive", Clients.Where(c => c.IsActive));
    }


    // получить данные об инструкторе в виде JSON
    public IActionResult GetClientData(int id) => Json(Clients.FirstOrDefault(i => i.Id == id));


    // метод для проверки обработки исключения
    public IActionResult ExceptionTest() => throw new Exception("ClientController Error");

    #endregion

    #region Обработка

    // загрузка данных из JSON
    public bool LoadData()
    {
        // если файла не существует
        if (System.IO.File.Exists(FileFullName))
        {
            Clients = JsonConvert.DeserializeObject<IEnumerable<Client>>(
                          System.IO.File.ReadAllText(FileFullName)) ??
                      new List<Client>();

            return true;
        }

        return false;
    }

    // сохранение данных в JSON
    public void SaveData() =>
        System.IO.File.WriteAllText(FileFullName, JsonConvert.SerializeObject(Clients));

    #endregion

    #endregion
}
