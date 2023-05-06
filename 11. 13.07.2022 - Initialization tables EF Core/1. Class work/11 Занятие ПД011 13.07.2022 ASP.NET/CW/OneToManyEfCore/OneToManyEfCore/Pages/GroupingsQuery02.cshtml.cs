using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class GroupingsQuery02Model : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public IQueryable<Result02> Results { get; private set; }

        // ������������ �������
        public string? Task { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public GroupingsQuery02Model(ApplicationContext db) {
            _context = db;
        } // GroupingsQuery02Model


        // ��� ������� ���� ��������� ������������ � �����������
        // ���� 1 ����������, ���������� ������� � ������
        public void OnGet() {
            Task = "����������� �� ���� \"��� �������\". ������������ � ����������� ���� 1 ����������, ���������� ������� � ������";

            Results = _context.Publications
                .AsNoTracking()
                .GroupBy(p => p.Category.CategoryName, 
                    (key, group) => new Result02(
                        key,
                        group.Min(p => p.Cost),
                        group.Max(p => p.Cost),
                        group.Count())
            );
        } // OnGet
    } // class GroupingsQuery02Model

    // ��� ������ ��������� ��������� �������
    public record Result02(string Category, int MinCost, int MaxCost, int Amount);
}
