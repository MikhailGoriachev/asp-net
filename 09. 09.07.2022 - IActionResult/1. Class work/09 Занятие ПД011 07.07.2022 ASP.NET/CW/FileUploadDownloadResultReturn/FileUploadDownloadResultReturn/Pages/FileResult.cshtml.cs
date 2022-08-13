using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileUploadDownloadResultReturn.Pages
{
    public class FileResultModel : PageModel
    {
        // ссылка на серверное окружение - для получения папки хоста
        // (чтобы сохранять там файлы)
        private IHostEnvironment _environment;

        // сообщение о результате работы
        public string Message { get; private set; } = "";

        // получение ссылки на серверное окружение при помощи
        // внедрения зависимости - через конструктор
        public FileResultModel(IHostEnvironment environment) {
            _environment = environment;
        } // FileResultModel
        

        // сюжетно не важно, прсто возвращает разметку страницы
        public void OnGet() {
            Message = "FileResult: готов к скачиванию";
        } // OnGet

        // скачивание файла по пути
        public IActionResult OnGetPhysicalFile() {
            string path = Path.Combine(_environment.ContentRootPath, "App_Data", "devs.jpg");
            string type = "application/octet-stream";   // text/plain для текста или универсальный тип application/octet-stream
            string name = "devs.jpg";

            return PhysicalFile(path, type, name);
        } // OnGetPhysicalFile


        // скачивание файла, как массива байтов
        public IActionResult OnGetBytesArray() {
            string path = Path.Combine(_environment.ContentRootPath, "App_Data", "devs.jpg");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string type = "application/octet-stream";   // text/plain для текста или универсальный тип application/octet-stream
            string name = "devs.jpg";

            // скачивание
            return File(mas, type, name);
        } // OnGetBytesArray


        // скачивание файла, как файлового потока
        public IActionResult OnGetFileStream() {
            string path = Path.Combine(_environment.ContentRootPath, "App_Data", "devs.jpg");
            FileStream fs = new FileStream(path, FileMode.Open);
            string type = "application/octet-stream";   // text/plain для текста или универсальный тип application/octet-stream
            string name = "devs.jpg";

            // скачивание
            return File(fs, type, name);
        } // OnGetFileStream


        // скачивание файла, через механизм виртуальных файлов 
        // все пути к файлам в данном случае будут сопоставляться с папкой wwwroot
        public IActionResult OnGetVirtualFile() {
            string path = Path.Combine("images", "bae-146.jpg");
            string type = "application/octet-stream";   // text/plain для текста или универсальный тип application/octet-stream
            string name = "bae-146.jpg";

            // скачивание
            return File(path, type, name);
        } // OnGetVirtualFile
    } // class FileResultModel
}
