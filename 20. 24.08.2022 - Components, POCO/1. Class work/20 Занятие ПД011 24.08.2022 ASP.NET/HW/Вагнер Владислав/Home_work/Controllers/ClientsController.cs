using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Home_work.Infrastructure;
using Home_work.Models.Clients;
using Newtonsoft.Json;
using System.IO;


namespace Home_work.Controllers;


public class ClientsController : Controller
{

    private IEnumerable<Client> _clients
    {
        get;
        set;
    }

    //Путь к файлу
    public string FilePath
    {
        get;
        set;
    }

    private string _clientsImagesPath { get; set; }

    //SelectList коллекции для выпадающих списков
    private SelectList _surnamesSelect;


    //Сортировки 
    public Dictionary<string, (string title, Func<IEnumerable<Client>> func)> SortFuncts { get; set; }

    //Выборки 
    public Dictionary<string, (string title, Func<IEnumerable<Client>> func)> SelectionFuncts { get; set; }


    public ClientsController()
    {
        FilePath = $"{Environment.CurrentDirectory}\\App_Data\\clients.json";
        _clientsImagesPath = @"wwwroot\images\clients";

        //Если файл есть - десериализация, в противном случае генерация
        if (System.IO.File.Exists(FilePath))
        {
            Desiralize();
        }
        else
        {
            _clients = Utils.GetClients();
            Serialize();
        }

        _surnamesSelect = new SelectList(_clients.Select(c => c.Surname).Distinct());

        //Задание методов сортировок
        SortFuncts = new()
        {
            ["OrderBySnp"] = ("Сортировка по алфавиту", () => _clients?.OrderBy(c => c.Surname)
                                                                        .ThenBy(c => c.Name)
                                                                        .ThenBy(c => c.Patronymic)),
            ["OrderByAgeDesc"] = ("Сортировка по убыванию возраста", () => _clients?.OrderByDescending(c => c.Age)),
            ["Default"] = ("Исходная коллекция", () => _clients),
        };

        //Определение делегатов выборок
        SelectionFuncts = new()
        {
            ["SelectBySurname"] = ("Выборка по фамилии", () => {
                string surname = Request.Form["Surname"];
                return _clients?.Where(c => c.Surname.ToLower() == surname.ToLower());
            }
            ),
            ["SelectConstant"] = ("Выборка постоянных клиентов", () => _clients.Where(c => c.IsConstant)),

            ["Default"] = ("Исходная коллекция", () => _clients),
        };


    }

    public IActionResult Default()
    {
        //Фамилии для выпадающего списка
        ViewBag.SelectSurnames = _surnamesSelect;
        ViewBag.Header = "Исходная коллекция";

        return View("ClientsView",_clients);
    }

    #region CRUD

    //Добавление
    public IActionResult AddClient()
    {
        ViewBag.AddingFormMode = true;

        //Корректный Id, если коллекция пуста
        int maxId = _clients.Count() > 0 ? _clients.Max(c => c.Id) : 0;

        //Создание объекиа
        return View("ClientsForm", new ClientBindingModel() {Id = maxId+1 });
    }

    //Редактирование
    public IActionResult EditClient(int value)
    {
        Client? client = _clients.FirstOrDefault(b => b.Id == value);

        //Если элемент не найден, то форму не запускаем
        if (client is null)
            return View("ClientsView", _clients);

        ViewBag.AddingFormMode = false;

        //Форма редактирования объекта
        return View("ClientsForm", new ClientBindingModel(client));
    }

    //Post-обработчик формы на редактирование и добавление 
    [HttpPost]
    [TypeFilter(typeof(ActionClientAttribute))]
    public IActionResult FormSubmit(ClientBindingModel model, bool addingmode, IFormFile? photoFile)
    {
        //Если модель не валидна, тогда запустить представление с формой
        if (!ModelState.IsValid){
            ViewBag.AddingFormMode = addingmode;
            return View("ClientsForm", model);
        }
        //Проверка e-mail на уникальность
        else if(_clients.Where(c => c.Id != model.Id).Select(c => c.Email).Contains(model.Email)){

            ViewBag.AddingFormMode = addingmode;
            ModelState.AddModelError(nameof(model.Email), $"{model.Email} уже занят!");
            return View("ClientsForm", model);
        }

        //Загрузить файл
        if (photoFile != null)
        {
            //Если получить и записать файл не удалось, тогда используем стандартное изображение
            model.PhotoFile = Utils.SaveFile(_clientsImagesPath, photoFile) ? photoFile.FileName : "default.jpg";
        }


        //Редактирование элемента / добавление в коллекцию
        if (addingmode)
        {
            //Кодирование пароля извне
            /*model.EncodePassword();*/

            //Происходит приведение к List
            var List = _clients.ToList();
            List.Add(new Client(model) ?? new Client());

            _clients = List;

        }
        else
        {
            Client editingClient = _clients.FirstOrDefault(c => c.Id == model.Id);

            if (editingClient != null)
            {
                editingClient.PhotoFile = model.PhotoFile;
                editingClient.Surname = model.Surname;
                editingClient.Name = model.Name;
                editingClient.Patronymic = model.Patronymic;
                editingClient.Age = model.Age;
                editingClient.PhoneNumber = model.PhoneNumber;
                editingClient.Email = model.Email;

                //Пока что пароль не редактируется
                /*editingClient.Password = model.Password;*/

                editingClient.IsConstant = model.IsConstant;
            }

        }

        Serialize();

        //Фамилии для выпадающего списка
        ViewBag.SelectSurnames = _surnamesSelect;
        ViewBag.Header = $"{(addingmode ? "Добавлен" : "Отредактирован")} клиент с Id: {model.Id}";


        //Если добавление - сортируем коллекцию по Id
        return View("ClientsView", addingmode ? _clients.OrderByDescending(b => b.Id) : _clients);
    }

    public IActionResult DeleteClient(int value)
    {
        Client? deletingClient = _clients.FirstOrDefault(b => b.Id == value);

        //Если элемент не найден, то уходим
        if (deletingClient is null)
            return View("ClientsView", _clients);

        _clients = _clients.Where(c => c.Id != value);

        Serialize();

        //Фамилии для выпадающего списка
        ViewBag.SelectSurnames = _surnamesSelect;
        ViewBag.Header = $"Удалён клиент с Id: {value}";

        //Удаление файла

        string fullPath = Path.Combine(_clientsImagesPath, deletingClient.PhotoFile ?? "");

        //Без удаления файла в случае, если файл default или файла не существует
        if (deletingClient.PhotoFile == "default.jpg" || !System.IO.File.Exists(fullPath))
        {
            return View("ClientsView", _clients);
        }

        System.IO.File.Delete(fullPath);

        return View("ClientsView", _clients);
    }

    #endregion

    #region Сортировки и выборки


    //Сортировки инструкторов 
    public IActionResult OrderBy(string value)
    {
        (string header, Func<IEnumerable<Client>> sort) tuple = SortFuncts.ContainsKey(value) ? SortFuncts[value] : SortFuncts["Default"];

        ViewBag.Header = tuple.header;

        //Фамилии для выпадающего списка
        ViewBag.SelectSurnames = _surnamesSelect;

        return View("ClientsView", tuple.sort());
    }

    //Выборки
    [HttpPost]
    [HttpGet]
    public IActionResult Selection(string value)
    {
        (string header, Func<IEnumerable<Client>> function) tuple = SelectionFuncts.ContainsKey(value) ? SelectionFuncts[value] : SelectionFuncts["Default"];

        ViewBag.Header = tuple.header;
        ViewBag.ShowControls = false;

        //Фамилии для выпадающего списка
        ViewBag.SelectSurnames = _surnamesSelect;

        return View("ClientsView", tuple.function());
    }

    //Выборка по возрастному диапазону
    public IActionResult SelectByAgeRange() => View("AgeRangeForm",new AgeRange());


    [HttpPost]
    public IActionResult SelectByAgeRange(AgeRange model)
    {
        if (!ModelState.IsValid)
            return View("AgeRangeForm", model);
        //Валидация на стороне сервера
        else if (model.Min >= model.Max)
        {
            ModelState.AddModelError(nameof(model.Min), "Минимальное значение должно быть < максимального");
            ModelState.AddModelError(nameof(model.Max), "Максимальное значение должно быть > минимального");
            return View("AgeRangeForm", model);
        }

        //Собственно выборка
        var selected = _clients.Where(r => r.Age >= model.Min && r.Age <= model.Max);
        ViewBag.Header = $"Выборка клиентов в возрасте {model.Min} - {model.Max} лет";

        ViewBag.ShowControls = false;

        //Фамилии для выпадающего списка
        ViewBag.SelectSurnames = _surnamesSelect;

        return View("ClientsView", selected);
    }

    #endregion

    #region JSON

    public IActionResult DeleteFile()
    {
        if (System.IO.File.Exists(FilePath))
            System.IO.File.Delete(FilePath);

        //Данные для выпадающих списков в модальных окнах
        ViewBag.SelectStartPoints = _surnamesSelect; ;

        return View("ClientsView", _clients);
    }

    //Сериализация 
    public void Serialize()
    {
        string json = JsonConvert.SerializeObject(_clients, Formatting.Indented);

        //Получение имени папки
        string directory = FilePath.Replace(FilePath.Substring(FilePath.LastIndexOf('\\')), "");

        //Есил каталога нет, то создать 
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        System.IO.File.WriteAllText(FilePath, json);
    }

    //Десериализация
    public void Desiralize()
    {
        string json = System.IO.File.ReadAllText(FilePath);

        //Если десериализация прошла не успешно тогда просто сгенерировать новую коллекцию
        _clients = JsonConvert.DeserializeObject<IEnumerable<Client>>(json) ?? Utils.GetClients();
    }

    #endregion


    //AJAX запрос
    public IActionResult GetClient(int id)
    {
        Client client = _clients.FirstOrDefault(c => c.Id == id);

        if (client == null)
            return Json(new Client());

        client.EncodePassword();

        return Json(client);
    }


    //Демонстрация работы фильтра исключений
    public IActionResult ExceptionDemo()
    {
        //Выход за переделы массива
        var clients = _clients.ToList()[int.MaxValue];

        return View("ClientsView", _clients);
    }

}
