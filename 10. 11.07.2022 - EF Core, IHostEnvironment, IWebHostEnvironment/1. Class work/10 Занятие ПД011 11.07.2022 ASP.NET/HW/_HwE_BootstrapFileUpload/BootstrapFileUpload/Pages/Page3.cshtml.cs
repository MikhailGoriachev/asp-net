using BootstrapFileUpload.Models.Task01;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BootstrapFileUpload.Pages
{
    // �������� 3.
    // ���������� �������� ��� ������ ���� ���������� ������� �������,
    // ���������� � ����� ����������

    public class Page3Model : PageModel
    {
        // ������ �� ��������� ��������� - ��� ��������� ����� �����
        // (����� ������ ���� ������)
        private IHostEnvironment _environment;

        public Page3Model(IHostEnvironment environment) {
            _environment = environment;
        }

        public IEnumerable<Goods>? DisplayGoods;
        public IEnumerable<string>? DisplayPhotos;

        // �� ������� GET �������� ��������� ������ ��� ����������� � �������  
        public void OnGet() {
            string json = System.IO.File.ReadAllText($"{_environment.ContentRootPath}/App_Data/Task01/goods.json");
            DisplayGoods = JsonConvert.DeserializeObject<List<Goods>>(json)!;

            // �������� ��� ����� ���� jpg �� ����� � ������������ ������� �������
            DisplayPhotos = Directory
                .GetFiles("wwwroot/images/task01/", "*.jpg")
                .Select(f => f.Replace("wwwroot/", ""));
        } // OnGet

    } // class Page3Model
}
