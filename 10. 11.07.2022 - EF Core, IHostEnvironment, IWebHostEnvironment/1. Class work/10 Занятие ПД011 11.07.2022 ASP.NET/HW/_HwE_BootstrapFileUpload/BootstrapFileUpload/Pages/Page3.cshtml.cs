using BootstrapFileUpload.Models.Task01;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BootstrapFileUpload.Pages
{
    // Страница 3.
    // Реализуйте карусель для вывода всех фотографий бытовой техники,
    // хранящихся в Вашем приложении

    public class Page3Model : PageModel
    {
        // ссылка на серверное окружение - для получения папки хоста
        // (чтобы читать файл данных)
        private IHostEnvironment _environment;

        public Page3Model(IHostEnvironment environment) {
            _environment = environment;
        }

        public IEnumerable<Goods>? DisplayGoods;
        public IEnumerable<string>? DisplayPhotos;

        // по запросу GET получить коллекцию данных для отображения в галерее  
        public void OnGet() {
            string json = System.IO.File.ReadAllText($"{_environment.ContentRootPath}/App_Data/Task01/goods.json");
            DisplayGoods = JsonConvert.DeserializeObject<List<Goods>>(json)!;

            // получить все файлы типа jpg из папки с фотографиями бытовой техники
            DisplayPhotos = Directory
                .GetFiles("wwwroot/images/task01/", "*.jpg")
                .Select(f => f.Replace("wwwroot/", ""));
        } // OnGet

    } // class Page3Model
}
