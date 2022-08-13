using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingPropsToParams.Pages
{
    [IgnoreAntiforgeryToken]
    public class Binding04Model : PageModel
    {
        // �������������� ���������� - Name = ������������
        [BindProperty(SupportsGet = true, Name = "eman")]
        public string? Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Age { get; set; }

        // ��� ���� ������ �������������� ���������
        [BindProperty(SupportsGet = true, Name= "earn")]
        public int Salary { get; set; }

        public void OnGet()
        {
        }
    }
}
