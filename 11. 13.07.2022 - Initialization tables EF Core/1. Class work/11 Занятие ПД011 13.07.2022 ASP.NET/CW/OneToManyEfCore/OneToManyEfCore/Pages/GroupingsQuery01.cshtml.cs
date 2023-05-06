using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class GroupingsQuery01Model : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ��������� ������� � ������� ��
        public IQueryable<Result01> Results { get; private set; }

        // ������������ �������
        public string? Task { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public GroupingsQuery01Model(ApplicationContext db)
        {
            _context = db;
        } // GroupingsQuery01Model


        // ��������� ����������� �� ���� ��� �������. ��� ������� ����
        // ��������� ������� ���� 1 ����������, ���������� ������� � ������
        public void OnGet() {
            Task = "����������� �� ���� \"��� �������\". ������� ���� 1 ����������, ���������� ������� � ������";
            
            // ��������� ��������� ������� ��� ����������� � ��������
            Results = _context.Publications
                .AsNoTracking()
                .GroupBy(p => p.Category.CategoryName,
                (key, group) => new Result01(
                    key, 
                    group.Average(p => p.Cost), 
                    group.Count())
                );
        } // OnGet
    }

    // ��� ������ ��������� ��������� �������
    public record Result01(string Category, double AvgCost, int Amount);
}
