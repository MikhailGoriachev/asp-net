using UsingFfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UsingFfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ���� ��� �������� � ��������� �����
        [BindProperty] public Publication? Publication { get; set; }

        // ����������� � ���������� ����������� ��� ���������
        // ������� � ���� ������
        public CreateModel(ApplicationContext db) {
            _context = db;
        } // CreateModel


        // ������ ������� �������� � ������ �����
        public void OnGet() { } // OnGet


        // ��������� ����� ����� � ������� ���������� ������������
        public async Task<IActionResult> OnPostAsync() {
            _context.Publications.Add(Publication);
            await _context.SaveChangesAsync();

            // ����� ���������� ������ � ������� �� ��������� 
            // �� �������� ����������� ���� ������� �������
            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class CreateModel
}
