using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    public class FilterQueriesModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public IQueryable<Travel> Travels { get; private set; } = null!;

        // ��������� ������������ �����
        [BindProperty]
        public string? NamePurpTravel { get; set; }

        // ���� �������
        [BindProperty]
        public int CostTransportServ { get; set; }

        // ������������ �������
        [BindProperty]
        public int Duration { get; set; }

        // ������ ����������
        [BindProperty]
        public string NameCountry { get; set; }

        // ����������� ��������� ����
        [BindProperty]
        public int CostVisaMin { get; set; }

        // ������������ ��������� ����
        [BindProperty]
        public int CostVisaMax { get; set; }

        // ������������ �������
        public string? Task { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public FilterQueriesModel(ApplicationContext db)
        {
            _context = db;
        } // FilterQueriesModel

        public void OnGet() { }

        // �������� �� ������� �������� ���������� � ��������� � ����� ������� �������
        public void OnPostQuery01()
        {
            Task = $"������� ���������� � ��������� � ����� ������� {NamePurpTravel}";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Route.PurposeTravel.NamePurpTravel == NamePurpTravel);
        } // OnPostQuery01


        // �������� �� ������� �������� ���������� � ���������,
        // ��� ������� ���� ������� �������� � ��������� ������������ �� ��������� 2000 ���.
        public void OnPostQuery02()
        {
            Task = $"������� ���������� � ��������� � ����� ������� {NamePurpTravel} � ��������� ������������ {CostTransportServ}�.";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Route.PurposeTravel.NamePurpTravel == NamePurpTravel && t.Route.Country.CostTransportServ == CostTransportServ);
        } // OnPostQuery02


        // �������� �� ������ ������� � ������� ���������� � ��������,
        // ����������� ������� � ����������� ���� ���������� � ������ �� ����� 10
        public void OnPostQuery03(int duration = 10)
        {
            Task = $"������� ���������� � ��������, � ����������� ���� ���������� � ������ �� ����� {duration}";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Duration >= duration);
        } // OnPostQuery03

        // �������� �� ������� �������� ���������� � ��������� � �������� ������.
        // ���������� �������� ������ �������� ��� ���������� �������
        public void OnPostQuery04()
        {
            Task = $"������� ���������� � ��������� � �������� ������ {NameCountry}";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Route.Country.NameCountry == NameCountry);
        } // OnPostQuery04

        // �������� �� ������� �������� ���������� � �������,
        // ��� ������� ��������� ���������� ���� ���� ��������
        // �� ���������� ���������. ������ � ������� ������� ���������
        // �������� ��� ���������� �������
        public void OnPostQuery05()
        {
            Task = $"������� ���������� � ������, ��������� ���������� ���� �� {CostVisaMin}�. �� {CostVisaMax}�.";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r!.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r!.PurposeTravel)
                .AsNoTracking()
                .Where(t => CostVisaMin <= t.Route.Country.CostVisa && t.Route.Country.CostVisa <= CostVisaMax);
        } // OnPostQuery05


    } // class FilterQueriesModel
}
