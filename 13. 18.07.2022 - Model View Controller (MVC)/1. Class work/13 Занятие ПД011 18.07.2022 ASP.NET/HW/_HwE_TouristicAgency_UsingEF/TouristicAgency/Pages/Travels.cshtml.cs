using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages
{
    // ������ � �������� �������
    public class TravelsModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<Travel>? Travels { get; private set; }

        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public TravelsModel(ApplicationContext db) {
            _context = db;
        } // TravelsModel


        // ����� �� GET-������, ������ ������� �������� � ���������� ������ � ��������
        public void OnGet() {
            // �������� ������ �� ��������� �������, ��������� �������� ��� ��������� ��������
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(t => t.Country)
                .Include(t => t.Route.Purpose)
                .AsNoTracking()
                .ToList();
        } // OnGet
    } // class TravelsModel
}
