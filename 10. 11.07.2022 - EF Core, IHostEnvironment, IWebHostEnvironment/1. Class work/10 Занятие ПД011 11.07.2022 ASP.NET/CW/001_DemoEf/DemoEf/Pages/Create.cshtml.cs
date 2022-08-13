using DemoEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoEf.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ���� ��� �������� � ��������� �����
        [BindProperty] public User? Person { get; set; }

        // ����������� � ���������� ����������� ��� ���������
        // ������� � ���� ������
        public CreateModel(ApplicationContext db) {
            _context = db;
        } // CreateModel


        // ������ ������� �������� � ������ �����
        public void OnGet() { } // OnGet


        // ��������� ����� ����� � ������� ���������� ������������
        public async Task<IActionResult> OnPostAsync() {
            _context.Users.Add(Person);
            await _context.SaveChangesAsync();

            // ����� ���������� ������ � ������� �� ��������� 
            // �� �������� ����������� ���� ������� �������
            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class CreateModel
}
