using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // ������������� ������� �� � ������������ �������� ������,
    // �������� � �������������� ������ 
    public class GetAllModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<Publication>? Publications { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public GetAllModel(ApplicationContext db) {
            _context = db;
        } // GetAllModel

        // �� GET-������ ������ ������� �������� � ����������
        // ���������� ������� �� ������� ��
        public void OnGet() {
            Publications = _context.Publications
                .Include(c => c.Category)
                .AsNoTracking()
                .ToList();
        } // OnGet

        // ���������� ����� �� ������� submit
        // �������� ������ �� ��������������
        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            var publication = await _context.Publications.FindAsync(id);

            if (publication != null) {
                _context.Publications.Remove(publication);
                await _context.SaveChangesAsync();
            } // if

            return RedirectToPage();
        } // OnPostDeleteAsync
    } //  class GetAllModel
}
