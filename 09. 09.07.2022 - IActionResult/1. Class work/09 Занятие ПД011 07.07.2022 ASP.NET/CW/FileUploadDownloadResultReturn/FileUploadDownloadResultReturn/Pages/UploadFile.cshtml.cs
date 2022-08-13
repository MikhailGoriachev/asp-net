using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileUploadDownloadResultReturn.Pages
{
    public class UploadFileModel : PageModel
    {
        // ������ �� ��������� ��������� - ��� ��������� ����� �����
        // (����� ��������� ��� �����)
        private IHostEnvironment _environment;

        // ��������� ������ �� ��������� ��������� ��� ������
        // ��������� ����������� - ����� �����������
        public UploadFileModel(IHostEnvironment environment) {
            _environment = environment;
        } // UploadFileModel

        // ����������� ���� ������ ������������ �� IFormFile
        [BindProperty] public IFormFile Upload { get; set; }

       

        // ���������� �������� �����
        public void OnPost() {
            var file = Path.Combine(_environment.ContentRootPath, "App_Data", Upload.FileName);

            using var fileStream = new FileStream(file, FileMode.Create);
            Upload.CopyTo(fileStream);
        }


        // ����������� �������� �����
        public async Task OnPostDemoAsync() {
            var file = Path.Combine(_environment.ContentRootPath, "App_Data", Upload.FileName);

            using var fileStream = new FileStream(file, FileMode.Create);
            await Upload.CopyToAsync(fileStream);
        }


        // ������� �� �����
        public void OnGet() { }
    }
}
