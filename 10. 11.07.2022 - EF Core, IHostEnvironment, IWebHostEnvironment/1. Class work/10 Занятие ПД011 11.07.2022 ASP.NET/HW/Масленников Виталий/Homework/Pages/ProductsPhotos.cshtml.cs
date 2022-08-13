using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages;

public class ProductsPhotos : PageModel
{
    // Директория со стандартными изображениями техники
    public string ImageFolder = "/images/appliances/";
    // Директория с загруженными изображениями техники
    public string UploadFolder = "/images/appliances/upload/";

    // Список всех хранимых изображений
    public string[] Images;
    
    // Конструктор
    public ProductsPhotos(IWebHostEnvironment env)
    {
        Images = Directory.GetFiles($"{env.WebRootPath}{ImageFolder}")
            .Select(i => $"{ImageFolder}{Path.GetFileName(i)}")
            .Concat(Directory.GetFiles($"{env.WebRootPath}{UploadFolder}")
                .Select(i => $"{UploadFolder}{Path.GetFileName(i)}"))
            .ToArray();
    }
}