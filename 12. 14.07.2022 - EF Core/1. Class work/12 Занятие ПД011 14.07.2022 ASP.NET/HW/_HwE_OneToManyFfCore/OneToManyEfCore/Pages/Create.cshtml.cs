using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ���� ��� �������� � ��������� �����
        [BindProperty] public Publication? Publication { get; set; }

        // �������� ������� ��� ����������� ������
        // SelectList(������������������������, �������������������, ����������������������)
        public SelectList Categories { get; private set; } = null!;

        // ����������� � ���������� ����������� ��� ���������
        // ������� � ���� ������
        public CreateModel(ApplicationContext db) {
            _context = db;
        } // CreateModel


        // ������ ������� �������� � ������ �����
        public void OnGet() {
            // �������� ������ �������� ��� ������������ ����������� ������
            Categories = new SelectList(_context.Categories.AsNoTracking().ToList(), "Id", "CategoryName");
        } // OnGet


        // ��������� ����� ����� � ������� ���������� ������������
        public async Task<IActionResult> OnPostAsync() {
            _context.Publications.Add(Publication);
            await _context.SaveChangesAsync();

            // ����� ���������� ������ � ������� �� ��������� 
            // �� �������� ����������� ���� ������� �������
            return RedirectToPage("GetPublicationsAll");
        } // OnPostAsync
    } // class CreateModel
}
