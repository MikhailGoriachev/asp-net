using UsingFfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UsingFfCore.Pages
{
    // �������� ��������������
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        [BindProperty] public Publication? Publication { get; set; }


        // ����������� � ���������� �����������, ������ � ��
        public EditModel(ApplicationContext db) {
            _context = db;
        } // EditModel


        // �� GET-������� ����� ������ � �������, ��������� HTML-��������
        // ����� �������, ���� �� ������ � �� �� ������� - ������� ���������
        // ��� 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Publication = await _context.Publications.FindAsync(id);
            return Publication != null?Page():NotFound();
        } // OnGetAsync


        // ��������� ������, ���������� �� �����
        public async Task<IActionResult> OnPostAsync() {
            // !!! ���� Id == 0 �� Update(), �� ������, � ��������� ����� ����� :(
            _context.Publications.Update(Publication);
            await _context.SaveChangesAsync();

            // ������� �� �������� ����������� ���� ������� - ��� �������
            // "�������� �����"
            return RedirectToPage("GetAll");
        } // OnPostAsync
    } // class EditModel
}
