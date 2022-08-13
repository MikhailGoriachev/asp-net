using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    public class GroupingsQuery03Model : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public IQueryable<Result03> Results { get; private set; }

        // ������������ �������
        public string? Task { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public GroupingsQuery03Model(ApplicationContext db)
        {
            _context = db;
        } // GroupingsQuery03Model


        // ��������� ����������� �� ���� ������������ ��������.
        // ��� ������� ����� ��������� ������� ���� 1 ����������
        public void OnGet()
        {
            Task = "����������� �� ���� \"������������ ��������\". ������� ���� 1 ����������, ���������� ������� � ������";

            Results = _context.Publications
                .AsNoTracking()
                .GroupBy(p => p.Duration,
                    (key, group) => new Result03(
                        key,
                        group.Average(p => p.Cost),
                        group.Count())
                );
        } // OnGet
    } // class GroupingsQuery02Model

    // ��� ������ ��������� ��������� �������
    public record Result03(int Duration, double AvgCost, int Amount);
}