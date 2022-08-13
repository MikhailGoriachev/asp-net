using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages;

public class ApplianceImages : PageModel
{
    // название файлов с изображениями
    public List<string> FileNameList { get; set; }

    // название папки с изображениями прибороа
    public string DirectoryName { get; set; } = "wwwroot\\images\\appliances";

    // ссылка на окружение
    private IHostEnvironment _environment;

    #region Конструкторы

    // конструктор инициализирующий
    public ApplianceImages(IHostEnvironment environment)
    {
        _environment = environment;
    }

    #endregion

    public void OnGet()
    {
        // получение списка файлов в папке с изображениями
        FileNameList = new List<string>(Directory.GetFileSystemEntries(Path.Combine(_environment.ContentRootPath, DirectoryName)))
            .Select(f => f.Substring(f.LastIndexOf('\\') + 1))
            .ToList();
    }
}
