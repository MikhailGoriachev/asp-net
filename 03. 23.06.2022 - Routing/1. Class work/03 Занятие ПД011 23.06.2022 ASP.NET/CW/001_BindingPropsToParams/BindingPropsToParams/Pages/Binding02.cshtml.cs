using BindingPropsToParams.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BindingPropsToParams.Pages
{
    
    // �������� � ������� ��������
    [IgnoreAntiforgeryToken]
    public class Binding02Model : PageModel
    {
        // �������� �������� ���� - ����������� �� ��������� ����� ����
        [BindProperty]
        public Person? Admin { get; set; } = new Person("�������", 23);

        public string Message { get; set; } = "";

        public void OnGet() {
            Message = "������� ������";
        }

        public void OnPost() {
            Message = $"���: {Admin.Name}, �������: {Admin.Age}";
        }
    }
}

