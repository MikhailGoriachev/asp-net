using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages;
public class Report01Model : PageModel
{
    // ������ �� ���� ������
    private ApplicationContext _context;

    // ������ �� ��������� ��������� - ������� �� ��
    public IQueryable<Result01> Rows { get; private set; } = null!;

    // ������������ �������
    public string? Task { get; private set; }


    // ��������� ������� � �� ��� ������ ������������
    // � ���������� �����������
    public Report01Model(ApplicationContext db) {
        _context = db;
    } // Report01Model


    // ��������� ����������� �� ���� ���� �������.  ����������
    // �����������, ������� � ������������ ��������� 1 ��� ����������
    public void OnGet() {
        Task = "��������� ����������� �� ���� \"���� �������\". ���������� " +
               "�����������, ������� � ������������ ��������� 1 ��� ����������";

        Rows = _context.Travels
            .Include(t => t.Route)
            .ThenInclude(r => r.Purpose)
            .AsNoTracking()
            .GroupBy(r => r.Route.Purpose.Name,
                (key, group) => new Result01(
                    key,
                    group.Min(t => t.Route.DailyCost),
                    group.Average(t => t.Route.DailyCost),
                    group.Max(t => t.Route.DailyCost),
                    group.Count()
                ));
    } // OnGet
} // class Report01Model

// ��� ������ ��������� ��������� ������� - ����������� record
public record Result01(string Purpose, int MinCost, double AvgCost, int MaxCost, int Amount);

