using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ActionResultsSamples.Controllers;
public class FilesController : Controller
{
    private readonly IWebHostEnvironment _environment;

    // получение информации об окружении (папки и т.п.) - внедрение
    // зависимостей, dependency injection
    public FilesController(IWebHostEnvironment environment) {
        _environment = environment;
    }

    public IActionResult Index() => View();

    // FileContentResult
    public IActionResult Sample1() {
        byte[] fileContent = System.IO.File.ReadAllBytes("App_Data/document.pdf");

        // Если указать имя третьим параметром - браузер будет сохранять
        // файл даже если поддерживает формат и может его открыть.
        // Если имя не указано - браузер скачивает и открывает файл, в том случае если
        // не поддерживает формат и не может его прочесть - сохраняет.
        return File(fileContent, "application/pdf", "Saved PDF File.pdf");
    }

    // FileStreamResult
    public IActionResult Sample2() {
        // Применяем если не хотим файл сохранять на сервере - например, сервер получает
        // файл из сети и транслирует его на клиента
        FileStream fileStream = System.IO.File.OpenRead("App_Data/document.pdf");
        return File(fileStream, "application/pdf");
    }

    // PhysicalFileResult
    public IActionResult Sample3() {
        // environment.ContentRootPath - абсолютный путь к директории в котором
        // хранится контент приложения.
        return PhysicalFile(_environment.ContentRootPath + @"\App_Data\document.pdf", "application/pdf");
    }
}
