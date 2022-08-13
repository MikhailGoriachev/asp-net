using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileUploadDownloadResultReturn.Pages
{
    public class FileResultModel : PageModel
    {
        // ������ �� ��������� ��������� - ��� ��������� ����� �����
        // (����� ��������� ��� �����)
        private IHostEnvironment _environment;

        // ��������� � ���������� ������
        public string Message { get; private set; } = "";

        // ��������� ������ �� ��������� ��������� ��� ������
        // ��������� ����������� - ����� �����������
        public FileResultModel(IHostEnvironment environment) {
            _environment = environment;
        } // FileResultModel
        

        // ������� �� �����, ����� ���������� �������� ��������
        public void OnGet() {
            Message = "FileResult: ����� � ����������";
        } // OnGet

        // ���������� ����� �� ����
        public IActionResult OnGetPhysicalFile() {
            string path = Path.Combine(_environment.ContentRootPath, "App_Data", "devs.jpg");
            string type = "application/octet-stream";   // text/plain ��� ������ ��� ������������� ��� application/octet-stream
            string name = "devs.jpg";

            return PhysicalFile(path, type, name);
        } // OnGetPhysicalFile


        // ���������� �����, ��� ������� ������
        public IActionResult OnGetBytesArray() {
            string path = Path.Combine(_environment.ContentRootPath, "App_Data", "devs.jpg");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string type = "application/octet-stream";   // text/plain ��� ������ ��� ������������� ��� application/octet-stream
            string name = "devs.jpg";

            // ����������
            return File(mas, type, name);
        } // OnGetBytesArray


        // ���������� �����, ��� ��������� ������
        public IActionResult OnGetFileStream() {
            string path = Path.Combine(_environment.ContentRootPath, "App_Data", "devs.jpg");
            FileStream fs = new FileStream(path, FileMode.Open);
            string type = "application/octet-stream";   // text/plain ��� ������ ��� ������������� ��� application/octet-stream
            string name = "devs.jpg";

            // ����������
            return File(fs, type, name);
        } // OnGetFileStream


        // ���������� �����, ����� �������� ����������� ������ 
        // ��� ���� � ������ � ������ ������ ����� �������������� � ������ wwwroot
        public IActionResult OnGetVirtualFile() {
            string path = Path.Combine("images", "bae-146.jpg");
            string type = "application/octet-stream";   // text/plain ��� ������ ��� ������������� ��� application/octet-stream
            string name = "bae-146.jpg";

            // ����������
            return File(path, type, name);
        } // OnGetVirtualFile
    } // class FileResultModel
}
