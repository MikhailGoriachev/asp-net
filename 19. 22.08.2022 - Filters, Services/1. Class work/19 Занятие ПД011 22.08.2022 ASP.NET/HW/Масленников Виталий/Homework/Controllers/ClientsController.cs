using Homework.Common;
using Homework.Models.TravelCompany;
using Homework.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using Utils = Homework.Common.Utils;

namespace Homework.Controllers;

[Route("Clients")]
public class ClientsController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly TravelCompany _travelCompany;
    private const int EncryptKey = 42;
    private const long FileSizeLimit = 2097152;
    private const string ImageFolder = "/images/clients/";


    // Конструктор
    public ClientsController(TravelCompany travelCompany, IWebHostEnvironment env)
    {
        _travelCompany = travelCompany;
        _env = env;
    }

    // Вывод сведений о клиентах в порядке хранения в файле
    [HttpGet("Index")]
    public IActionResult Index() => View(new ClientsIndexViewModel{ Clients = _travelCompany.Clients });

    
    // Вывод сведений о клиентах в порядке, заданном параметрами 
    [HttpGet("OrderBy/{property}/{sort}")]
    public IActionResult OrderBy(string property, string sort) => 
        View("Index", new ClientsIndexViewModel{ Clients = _travelCompany.Clients.OrderBySort(property, sort) });

    // Вывод сведений о клиентах с заданным диапазоном возраста
    [HttpPost("WhereAge")]
    public IActionResult WhereAge(int from, int to)
    {
        if (!ModelState.IsValid)
            return View("Index", new ClientsIndexViewModel{ Clients = _travelCompany.Clients });
        
        return View("Index", new ClientsIndexViewModel { 
            Clients = _travelCompany.Clients.Where(x => x.Age >= from && x.Age <= to)
        });
    }
    
    // Вывод сведений о клиентах по введенной фамиоии
    [HttpPost("WhereSurname")]
    public IActionResult WhereSurname(string surname) =>
        View("Index", new ClientsIndexViewModel{ Clients = _travelCompany.Clients.Where(x => 
            x.Surname.ToLowerInvariant().StartsWith(surname.ToLowerInvariant())) });

    // Вывод сведений о клиентах по признаку постоянства
    [HttpPost("WhereRegular")]
    public IActionResult WhereRegular(string regularity) =>
        View("Index", new ClientsIndexViewModel{
            Clients = _travelCompany.Clients.Where(x => 
                x.IsRegular == (regularity == "regular"))
        });

    // Переход на форму добавления нового клиента
    [HttpGet("AddClient")]
    public IActionResult AddClient()
    {
        Client client = new() { Id = 0 };
        return View("ClientForm", new ClientInputForm{ Client = client });
    }
    
    // Переход на форму редактирования данных клиента
    [HttpGet("EditClient/{id:int}")]
    public IActionResult EditClient(int id)
    {
        var client = _travelCompany.Clients.First(b => b.Id == id);
        string rawPassword = Utils.EncryptDecrypt(client.Password!, EncryptKey);
        
        return View("ClientForm", new ClientInputForm
        {
            Client = client, RawPassword = rawPassword, ConfirmRawPassword = rawPassword
        });
    }
    
    
    // Обновить/внести данные о клиенте
    [HttpPost("UpdateClient")]
    public async Task<IActionResult> UpdateClientAsync(ClientInputForm inputModel)
    {
        int index = _travelCompany.Clients.ToList().FindIndex(c => c.Id == inputModel.Client.Id);

        // Если клиент создаётся или изменяется и загружено изоображение
        if ((index > -1 && inputModel.File is not null) || index < 0)
        {
            try
            {
                if (inputModel.File == null)
                    throw new Exception("Файл фотографии не выбран");

                var extension = Path.GetExtension(inputModel.File.FileName).ToLowerInvariant();

                if (!ImageValidator.IsValidExtension(extension))
                    throw new Exception($"Не поддерживаемое расширение файла {extension}");

                if (inputModel.File.Length > FileSizeLimit)
                    throw new Exception($"Недопустим размер файла более {FileSizeLimit / 1048576 }Mb");

                if (!ImageValidator.IsValidSignature(inputModel.File))
                    throw new Exception("Некорректный файл изображения");

                var newFileName = $"{new DateTime().GetTimestamp()}{extension}";
                var uploaded = Path.Combine($"{_env.WebRootPath}{ImageFolder}", newFileName);

                await using var fileStream = new FileStream(uploaded, FileMode.Create);
                await inputModel.File.CopyToAsync(fileStream);

                inputModel.Client.Photo = newFileName;
            }catch (Exception ex)
            {
                ModelState.AddModelError(nameof(inputModel.File), ex.Message);
            }
        }
        else
        {
            // Иначе остаётся прежнее фото
            inputModel.Client.Photo = inputModel.Photo;
        }

        if(!ModelState.IsValid)
            return View("ClientForm", inputModel);

        inputModel.Client.Password = Utils.EncryptDecrypt(inputModel.RawPassword!, EncryptKey);
        
        _travelCompany.AddOrUpdateClient(inputModel.Client);
        _travelCompany.SaveData();
        return View("Index", new ClientsIndexViewModel{ Clients = _travelCompany.Clients });
    }
    
    // Удаление клиента из коллекции
    [HttpGet("RemoveClient/{id:int}")]
    public IActionResult RemoveClient(int id)
    {
        var client = _travelCompany.Clients.FirstOrDefault(c => c.Id == id);
        if (client is not null)
        {
            _travelCompany.RemoveClient(id);
            System.IO.File.Delete(Path.Combine($"{_env.WebRootPath}{ImageFolder}", client.Photo!));
            _travelCompany.SaveData();
        }
        return View("Index", new ClientsIndexViewModel{ Clients = _travelCompany.Clients });
    }

    // Получить данные о клиенте в JSON-формате
    [HttpGet("ClientInfo/{id:int}")]
    public IActionResult ClientInfo(int id)
    {
        var client = _travelCompany.Clients.FirstOrDefault(c => c.Id == id);
        return Json(client is not null
            ? new
            {
                client.Id,
                client.ShortName,
                client.Surname,
                client.Name,
                client.Patronymic,
                client.Age,
                client.Email,
                client.PhoneNumber,
                client.IsRegular,
                password = Utils.EncryptDecrypt(client.Password!, EncryptKey)
            }
            : new { error = "Not Found" });
    }
}