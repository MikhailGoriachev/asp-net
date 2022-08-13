using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SectionsDemo.Models;


namespace SectionsDemo.Pages
{
    // ������������� ���������� �������� 
    public class Page03Model : PageModel
    {
        // ������ ��� ����������� ������
        static List<Enterprise> enterprises { get; } = new() {
            new Enterprise(1, "Apple"),
            new Enterprise(2, "Samsung"),
            new Enterprise(3, "Google"),
            new Enterprise(4, "Motorola"),
            new Enterprise(5, "Xiaomi"),
        };

        // �������� ������� ��� ����������� ������
        // SelectList(������������������������, �������������������, ����������������������)
        public SelectList Companies { get; } = new SelectList(enterprises, "Id", "Name");

        [BindProperty]
        public Product Goods { get; set; } = new("", 1000, 0);
        public string Message { get; private set; } = "���������� ������";

        public void OnPost() {
            // �������� ������������� ������ - Goods.EnterpriseId �������� � ���������� �������� ������
            Enterprise? enterprise = enterprises.FirstOrDefault(e => e.Id == Goods.EnterpriseId);
            Message = $"�������� ����� �����: {Goods.Name} ({enterprise?.Name}) {Goods.Price} ���.";
        } // OnPost

        public record class Product(string Name, int Price, int EnterpriseId);
        
    }
}
