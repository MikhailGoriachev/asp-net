using DemoEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoEf.Pages
{
    // �������� ��������������
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        [BindProperty] public User? Person { get; set; }


        // ����������� � ���������� �����������, ������ � ��
        public EditModel(ApplicationContext db) {
            _context = db;
        } // EditModel


        // �� GET-������� ����� ������ � �������, ��������� HTML-��������
        // ����� �������, ���� �� ������ � �� �� ������� - ������� ���������
        // ��� 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Person = await _context.Users.FindAsync(id);
            return Person != null?Page():NotFound();
        } // OnGetAsync


        // ��������� ������, ���������� �� �����
        public async Task<IActionResult> OnPostAsync() {
            // !!! ���� Id == 0 �� Update(), �� ������, � ��������� ����� ����� :(
            _context.Users.Update(Person);
            await _context.SaveChangesAsync();

            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class EditModel
}
