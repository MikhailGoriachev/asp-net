using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoutingIntro.Pages
{
    public class Page03Model : PageModel
    {
        public int? Id { get; set; }

        // �������� � ������ id ������ � ��������, � ������, � ������ �������
        public void OnGet(int? id)
        {
            Id = id;
        }
    }
}
