using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CategoryEditModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        [BindProperty] public Category? Category { get; set; }


        // ����������� � ���������� �����������, ������ � ��
        public CategoryEditModel(ApplicationContext db) {
            _context = db;
        } // CategoryEditModel


        // �� GET-������� ����� ������ � �������, ��������� HTML-��������
        // ����� �������, ���� �� ������ � �� �� ������� - ������� ���������
        // ��� 404
        public async Task<IActionResult> OnGetAsync(int id) {
            Category = await _context.Categories.FindAsync(id);

            if (Category == null) {
                return NotFound();
            } // if
            return Page();
        } // OnGetAsync


        // ��������� ������, ���������� �� �����
        public async Task<IActionResult> OnPostAsync() {
            // !!! ���� Id == 0 �� Update(), �� ������, � ��������� ����� ����� :(
            _context.Categories.Update(Category);
            await _context.SaveChangesAsync();

            // ������� �� �������� ����������� ���� ������� - ��� �������
            // "�������� �����"
            return RedirectToPage("GetCategoriesAll");
        } // OnPostAsync
    } // class CategoryEditModel
}
