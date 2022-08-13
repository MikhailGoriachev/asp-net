using Homework.Common;
using Homework.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages
{
    public class ProductsModel : PageModel
    {
        private IWebHostEnvironment _env;
        
        private readonly long _fileSizeLimit;
        
        // Путь к стандартным изображениям в статическом контенте
        public string ImageFolder = "/images/appliances/";
        
        // Путь к загружаемым изображениям в статическом контенте
        public string UploadFolder = "/images/appliances/upload";
        
        // Получение пути к файлу - если отсутствует в стандартных изображениях, вернуть путь к загруженным
        public string GetImagePath(string fileName) =>
            Path.Combine(System.IO.File.Exists(Path.Combine($"{_env.WebRootPath}{ImageFolder}", fileName)) ?
                ImageFolder : UploadFolder, fileName);

        // Строка для сообщений об ошибках
        public string? ErrMsg;

        // Класс, хранящий коллекцию бытовой техники
        public static Company Company { get; private set; } = null!;

        // Отображаемые данные
        public IEnumerable<Product>? DisplayProducts;

        // Значения для выпадающего списка
        public SelectList Types;

        // Объект для добавления
        [BindProperty] public Product Product { get; set; } =
            new("Наименование", "АРТ00", "Аэрогриль", 1000, 1);

        // Загружаемый файл
        [BindProperty] public IFormFile? Upload { get; set; }
        
        public ProductsModel(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            Company = new Company(Path.Combine(_env.ContentRootPath, config.GetValue<string>("AppDataFolder")));
            DisplayProducts = Company.Products;
            Types  = new SelectList(Company.TypeImages, "Key", "Key");
        }

        public void OnGetOrderBy(string prop, string order = "Asc") =>
            DisplayProducts = Company.GetOrdered(prop, order);

        public async Task OnPostAddAsync()
        {
            try
            {
                if (Upload is not null)
                {
                    var extension = Path.GetExtension(Upload.FileName).ToLowerInvariant();
                    
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

                    Product.Image = newFileName;
                }

                Company.Add(new Product(Product));
                Company.Serialize();
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
            }
        }
    }
}