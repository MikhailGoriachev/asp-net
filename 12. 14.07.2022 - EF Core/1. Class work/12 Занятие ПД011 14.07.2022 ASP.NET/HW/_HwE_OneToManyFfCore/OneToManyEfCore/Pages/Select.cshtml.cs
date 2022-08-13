using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class SelectModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public IQueryable<Publication> Publications { get; private set; } = null!;

        // ������������� �������
        public string? Task { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public SelectModel(ApplicationContext db) {
            _context = db;
        } // SelectModel

        public void OnGet() { }

        // �������� ���������� �� ������� � �������� ��������.
        public void OnPostQuery01(string pubIndex = "464885") {
            Task = $"������� ���������� �� ������� � �������� {pubIndex}";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.PubIndex == pubIndex);
        } // OnPostQuery01


        // �������� ���������� ��� ���� ��������, ��� ������� ���� 1 ����������
        // ���� �������� �� ���������� ���������.
        public void OnPostQuery02(int costFrom = 300, int costTo = 1200) {
            Task = $"������� ���������� �� �������� � ����� �� ��������� {costFrom}, �, {costTo}";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => costFrom <= p.Cost && p.Cost <= costTo);
        } // OnPostQuery02


        // �������� ���������� �� ��������, ��� ������� "������������ ��������"
        // ���� �������� �� ���������� ���������.
        public void OnPostQuery03(int durationFrom = 6, int durationTo = 12) {
            Task = $"������� ���������� �� �������� � ������������� �������� �� ��������� {durationFrom}, �, {durationTo}";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => durationFrom <= p.Duration && p.Duration <= durationTo);
        } // OnPostQuery03


        // �������� ���������� �� �������� � �������� ��������� � ���� "��� �������"
        public void OnPostQuery04(string categoryName = "������") {
            Task = $"������� ���������� �� �������� � ����� ������� \"{categoryName}\"";
            Publications = _context.Publications
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.Category.CategoryName == categoryName);
        } // OnPostQuery04
    } // class QueriesModel
}
