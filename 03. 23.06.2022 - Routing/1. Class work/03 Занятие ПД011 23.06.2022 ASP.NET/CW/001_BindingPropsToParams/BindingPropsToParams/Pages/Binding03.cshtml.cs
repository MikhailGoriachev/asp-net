using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingPropsToParams.Pages
{
    // �������� ������� � GET-����������

    [IgnoreAntiforgeryToken]
    public class Binding03Model : PageModel
    {
        // ��� ��������� ������ �� GET-�������: SupportsGet = true
        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        // ��� ��������� ������ �� GET-�������: SupportsGet = true
        [BindProperty(SupportsGet = true)]
        public int Salary { get; set; } 


        public void OnGet()
        {
        }
    }
}
