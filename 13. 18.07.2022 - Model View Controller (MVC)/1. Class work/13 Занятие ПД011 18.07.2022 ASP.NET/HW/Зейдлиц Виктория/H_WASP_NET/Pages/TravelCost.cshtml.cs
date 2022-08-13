using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    public class TravelCostModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        public IQueryable<CalculateTravel>? CalculateTravels { get; set; }

        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public TravelCostModel(ApplicationContext db)
        {
            _context = db;
        } // TravelCostModel

        // ��������� ��� ������ ������� �� ������ ��������� � ���.
        // �������� ���� ������ ����������, ���� �������, ���� ������ �������,
        // ���������� ���� ����������, ������ ��������� �������.
        // ���������� �� ���� ������ ���������� (t.Route.CostDayStay * t.Duration) + t.Route.Country.CostTransportServ + 
        public void OnGet()
        {
            CalculateTravels = _context.Travels
                .Include(t => t.Route)
                .ThenInclude(r => r!.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r!.PurposeTravel)
                .Select(t => new CalculateTravel
                (
                   t.Route!.Country!.NameCountry!,
                   t.Route.PurposeTravel!.NamePurpTravel!,
                   t.StartTravel,
                   t.Duration,
                   t.Route.Country.CostVisa
                ))
                .OrderBy(t => t.City);
        } // OnGet

        public record CalculateTravel(string City, string Purpose, DateTime Date, int Duration, int Amount);

    } // class TravelCostModel
}
