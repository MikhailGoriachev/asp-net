using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// ���������� Newtonsoft ��� ������/������ � ������� JSON
using Newtonsoft.Json;

using SectionsDemo.Models;

namespace SectionsDemo.Pages
{
    // �������� � ���-�������
    public class Page02Model : PageModel
    {
        // �������� ����� ����� � ��������
        [BindProperty]
        public Product Product { get; set; } = new("��� ���������", 1230, "������-�������");

        public string Message { get; private set; } = "���������� ������";

        // ������ ��� ����� ��� ������/������
        private string _path = $"{Environment.CurrentDirectory}/App_Data/product.json";


        // ��������� GET-�������
        public void OnGet() {
            // ������ �� ����� � ������� JSON
            if (System.IO.File.Exists(_path)) {
                string json = System.IO.File.ReadAllText(_path);
                Product = JsonConvert.DeserializeObject<Product>(json);
            } // if
        } // OnGet


        // ���������� �����
        public void OnPost() {
            Message = $"�������� � �������� ����� �����: {Product.Name} ({Product.Company}), {Product.Price} ���.";

            // ������ � JSON-�������
            string json = JsonConvert.SerializeObject(Product);
            System.IO.File.WriteAllText(_path, json);
        } // OnPost
    } // class Page02Model 
}
