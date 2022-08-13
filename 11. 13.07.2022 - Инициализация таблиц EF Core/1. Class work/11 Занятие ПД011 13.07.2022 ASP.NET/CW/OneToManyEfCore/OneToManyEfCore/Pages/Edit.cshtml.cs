using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // �������� ��������������
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        [BindProperty] public Publication? Publication { get; set; }

        // �������� ������� ��� ����������� ������
        // SelectList(������������������������, �������������������, ����������������������)
        public SelectList Categories { get; private set; } = null!;

        // ����������� � ���������� �����������, ������ � ��
        public EditModel(ApplicationContext db) {
            _context = db;
        } // EditModel


        // �� GET-������� ����� ������ � �������, ��������� HTML-��������
        // ����� �������, ���� �� ������ � �� �� ������� - ������� ���������
        // ��� 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Publication = await _context.Publications.FindAsync(id);

            if (Publication == null) {
                return NotFound();
            } // if

            // �������� ������ �������� ��� ������������ ����������� ������
            Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "CategoryName");
            return Page();
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
