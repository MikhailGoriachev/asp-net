using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages;

public class ShipsPhotos : PageModel
{
    // Директория со стандартными изображениями техники
    public string ImageFolder = "/images/ships/";
    // Директория с загруженными изображениями техники
    public string UploadFolder = "/images/ships/upload/";

    // Список всех хранимых изображений
    public string[] Images;
    
    // Конструктор
    public ShipsPhotos(IWebHostEnvironment env)
    {
        Images = Directory.GetFiles($"{env.WebRootPath}{ImageFolder}")
            .Select(i => $"{ImageFolder}{Path.GetFileName(i)}")
            .Concat(Directory.GetFiles($"{env.WebRootPath}{UploadFolder}")
                .Select(i => $"{UploadFolder}{Path.GetFileName(i)}"))
            .ToArray();
    }
}