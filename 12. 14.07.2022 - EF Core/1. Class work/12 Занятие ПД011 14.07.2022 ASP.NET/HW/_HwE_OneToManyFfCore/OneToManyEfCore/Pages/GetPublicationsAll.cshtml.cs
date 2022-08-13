
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // ������������� ������� �� � ������������ �������� ������,
    // �������� � �������������� ������ 
    public class GetPublicationsAllModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<Publication>? Publications { get; private set; }

        // ������ �������� ��������� ������� - ��� ����������� � �����
        // ����� ���������� ������� 1
        public SelectList PubIndeces { get; private set; } = null!;

        // ������ ����� ��������� ������� - ��� ����������� � �����
        // ����� ���������� ������� 4
        public SelectList CategoryNames { get; private set; } = null!;

        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public GetPublicationsAllModel(ApplicationContext db) {
            _context = db;
        } // GetPublicationsAllModel

        // �� GET-������ ������ ������� �������� � ����������
        // ���������� ������� �� ������ ��
        public void OnGet() {
            Publications = _context.Publications
                .Include(c => c.Category)
                .AsNoTracking()
                .ToList();
			
			// �������� ������� ��������� ������� 
			// (������������, ��� ����� ����� ������ �� ��� ��������, 
			// ��� ���� � �������)
			PubIndeces = new SelectList(Publications.Select(p => p.PubIndex).Distinct());

            // �������� ������ �������� ����� ������� � ������ ������ 
            CategoryNames = new SelectList(_context.Categories.AsNoTracking().Select(c => c.CategoryName));
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
    } //  class GetPublicationsAllModel
}
