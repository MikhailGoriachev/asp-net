using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CategoryCreateModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ���� ��� �������� � ��������� �����
        [BindProperty] public Category? Category { get; set; }


        // ����������� � ���������� ����������� ��� ���������
        // ������� � ���� ������
        public CategoryCreateModel(ApplicationContext db) {
            _context = db;
        } // CategoryCreateModel


        // ������ ������� �������� � ������ �����
        public void OnGet() {} // OnGet


        // ��������� ����� ����� � ������� ��������� ���������
        public async Task<IActionResult> OnPostAsync() {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            // ����� ���������� ������ � ������� �� ��������� 
            // �� �������� ����������� ���� ������� �������
            return RedirectToPage("GetCategoriesAll");
        } // OnPostAsync
    } // class CategoryCreateModel
}
