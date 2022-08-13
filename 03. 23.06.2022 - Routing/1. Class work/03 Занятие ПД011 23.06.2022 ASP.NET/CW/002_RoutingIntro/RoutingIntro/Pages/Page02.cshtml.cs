using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoutingIntro.Pages.Shared
{
    public class Page02Model : PageModel
    {
        public int Id { get; set; }

        // �������� � ������ id ������ � ��������, � ������, � ������ �������
        public void OnGet(int id) {
            Id = id;
        }
    }
}
