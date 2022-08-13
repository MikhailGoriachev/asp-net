using Homework.Models.Ships;
using Homework.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages;

public class Ships : PageModel
{
    private IWebHostEnvironment _env;

    private readonly long _fileSizeLimit;

    // Путь к стандартным изображениям в статическом контенте
    public string ImageFolder = "/images/ships/";

    // Путь к загружаемым изображениям в статическом контенте
    public string UploadFolder = "/images/ships/upload";

    // Получение пути к файлу - если отсутствует в стандартных изображениях, вернуть путь к загруженным
    public string GetImagePath(string fileName) =>
        Path.Combine(System.IO.File.Exists(Path.Combine($"{_env.WebRootPath}{ImageFolder}", fileName))
                ? ImageFolder
                : UploadFolder, fileName);

    // Строка для сообщений об ошибках
    public string? ErrMsg;

    // Класс, хранящий коллекцию данных о кораблях
    public static Fleet Fleet { get; private set; } = null!;

    // Отображаемые данные
    public IEnumerable<Ship>? DisplayShips;

    // Объект для добавления
    [BindProperty] public Ship Ship { get; set; } = new();

    // Загружаемый файл
    [BindProperty] public IFormFile? Upload { get; set; }

    public Ships(IWebHostEnvironment env, IConfiguration config)
    {
        _env = env;
        _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        Fleet = new Fleet(Path.Combine(_env.ContentRootPath, config.GetValue<string>("AppDataFolder")));
        DisplayShips = Fleet.Ships;
    }

    public void OnGetOrderBy(string prop, string order = "Asc")
    {
        if (prop != "Source")
            DisplayShips = Fleet.GetOrdered(prop, order);
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        try
        {
            var validated = new Ship(Ship);
            
            var extension = Path.GetExtension(Upload!.FileName).ToLowerInvariant();

            if (!ImageValidator.IsValidExtension(extension))
                throw new Exception($"Не поддерживаемое расширение файла {extension}");

            if (Upload.Length > _fileSizeLimit)
                throw new Exception($"Недопустим размер файла более 2Mb");

            if (!ImageValidator.IsValidSignature(Upload))
                throw new Exception("Некорректный файл изображения");

            var newFileName = $"{new DateTime().GetTimestamp()}{extension}";

            var file = Path.Combine($"{_env.WebRootPath}{UploadFolder}", newFileName);

            await using var fileStream = new FileStream(file, FileMode.Create);
            await Upload.CopyToAsync(fileStream);

            Ship.Image = newFileName;

            Fleet.Add(validated);
            Fleet.Serialize();
        }
        catch (Exception ex)
        {
            ErrMsg = ex.Message;
            return RedirectToPage("Ships", "AddErr", new
            {
                Ship.Name, Ship.Type, Ship.Length, Ship.Width, Ship.Displacement, Ship.Year, ErrMsg
            });
        }
        
        return Page();
    }

    public void OnGetAddErr(string name, string type, double length, double width, double displacement, int year,
string errMsg)
    {
        Ship = new Ship(name, type, length, width, displacement, year);
        ErrMsg = errMsg;
    }
    
}