using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages;

public class Report02Model : PageModel
{
    // ������ �� ���� ������
    private ApplicationContext _context;

    // ������ �� ��������� ��������� - ������� �� ��
    public IQueryable<Result02> Rows { get; private set; } = null!;

    // ������������ �������
    public string? Task { get; private set; }


    // ��������� ������� � �� ��� ������ ������������
    // � ���������� �����������
    public Report02Model(ApplicationContext db)
    {
        _context = db;
    } // Report02Model


    // ��������� ����������� �� ���� ������ ����������. ��� ������ ������
    // ��������� ������� �������� �� ���� ��������� ������������ �����
    public void OnGet()
    {
        Task = "��������� ����������� �� ���� \"������ ����������\". ��� ������ ������ " +
               "��������� ������� �������� �� ���� \"��������� ������������ �����\"";

        Rows = _context.Travels
            .Include(t=> t.Route)
            .ThenInclude(t=> t.Country)
            .AsNoTracking()
            .GroupBy(t => t.Route.Country.Name,
                (key, group) => new Result02(
                    key,
                    group.Average(t => t.Route.Country.TransferCost),
                    group.Count()
                ));
    } // OnGet
} // class Report02Model

// ��� ������ ��������� ��������� �������
public record Result02(string Country, double AvgCost, int Amount);

