using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileUploadDownloadResultReturn.Pages
{
    public class UploadFileModel : PageModel
    {
        // ссылка на серверное окружение - для получения папки хоста
        // (чтобы сохранять там файлы)
        private IHostEnvironment _environment;

        // получение ссылки на серверное окружение при помощи
        // внедрения зависимости - через конструктор
        public UploadFileModel(IHostEnvironment environment) {
            _environment = environment;
        } // UploadFileModel

        // загружаемый файл должен отображаться на IFormFile
        [BindProperty] public IFormFile Upload { get; set; }

       

        // синхронная загрузка файла
        public void OnPost() {
            var file = Path.Combine(_environment.ContentRootPath, "App_Data", Upload.FileName);

            using var fileStream = new FileStream(file, FileMode.Create);
            Upload.CopyTo(fileStream);
        }


        // асинхронная загрузка файла
        public async Task OnPostDemoAsync() {
            var file = Path.Combine(_environment.ContentRootPath, "App_Data", Upload.FileName);

            using var fileStream = new FileStream(file, FileMode.Create);
            await Upload.CopyToAsync(fileStream);
        }


        // сюжетно не важно
        public void OnGet() { }
    }
}
