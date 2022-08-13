using BootstrapFileUpload.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BootstrapFileUpload.Pages
{
    // �������� 4.
    // ���������� �������� ��� ������ ���� ���������� ��������,
    // ���������� � ����� ����������
    public class Page4Model : PageModel
    {
        // ������ �� ��������� ��������� - ��� ��������� ����� �����
        // (����� ������ ���� ������)
        private IHostEnvironment _environment;

        public Page4Model(IHostEnvironment environment) {
            _environment = environment;
        }

        public IEnumerable<Ship>? DisplayShips;
        public IEnumerable<string>? DisplayPhotos;

        public void OnGet() {
            string json = System.IO.File.ReadAllText($"{_environment.ContentRootPath}/App_Data/Task02/fleet.json");
            DisplayShips = JsonConvert.DeserializeObject<List<Ship>>(json)!;

            // �������� ��� ����� ���� jpg �� ����� � ������������ ��������
            DisplayPhotos = Directory
                .GetFiles("wwwroot/images/task02/", "*.jpg")
                .Select(f => f.Replace("wwwroot/", ""));
        }
    } // class Page4Model
}
