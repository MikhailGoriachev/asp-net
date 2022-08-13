using BootstrapFileUpload.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BootstrapFileUpload.Pages
{
    // Страница 4.
    // Реализуйте карусель для вывода всех фотографий кораблей,
    // хранящихся в Вашем приложении
    public class Page4Model : PageModel
    {
        // ссылка на серверное окружение - для получения папки хоста
        // (чтобы читать файл данных)
        private IHostEnvironment _environment;

        public Page4Model(IHostEnvironment environment) {
            _environment = environment;
        }

        public IEnumerable<Ship>? DisplayShips;
        public IEnumerable<string>? DisplayPhotos;

        public void OnGet() {
            string json = System.IO.File.ReadAllText($"{_environment.ContentRootPath}/App_Data/Task02/fleet.json");
            DisplayShips = JsonConvert.DeserializeObject<List<Ship>>(json)!;

            // получить все файлы типа jpg из папки с фотографиями кораблей
            DisplayPhotos = Directory
                .GetFiles("wwwroot/images/task02/", "*.jpg")
                .Select(f => f.Replace("wwwroot/", ""));
        }
    } // class Page4Model
}
