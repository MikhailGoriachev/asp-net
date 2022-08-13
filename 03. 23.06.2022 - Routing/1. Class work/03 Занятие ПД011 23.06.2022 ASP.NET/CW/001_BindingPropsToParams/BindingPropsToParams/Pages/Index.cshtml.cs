using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingPropsToParams.Pages
{
    // �������� ������� ������ � ���������� �����
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        // ������� �������� �������� � ��������� �����
        [BindProperty]
        public string Name { get; set; } = "";

        // ������� �������� �������� � ��������� ������
        [BindProperty]
        public int Age { get; set; }

        public string Message { get; private set; } = "";


        public void OnGet() {
            Message = "������� ������";
        }

        // �������� ���������� onPost() �� �����, �.�. ��������� ��������
        public void OnPost() {
            Message = $"���: {Name}, �������: {Age}";
        }
    } // class IndexModel
}
