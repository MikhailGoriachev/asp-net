using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]

    public class TravelsAllModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<Travel>? Travels { get; private set; }

        // ������ ���� �������
        // ����� ���������� ������� 1
        public SelectList NamePurpose{ get; private set; } = null!;

        // ������ ����� ����������
        // ����� ���������� ������� 4
        public SelectList NameCountry{ get; private set; } = null!;


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public TravelsAllModel(ApplicationContext db)
        {
            _context = db;
        } // TravelsAllModel

        // �� GET-������ ������ ������� �������� � ����������
        // ���������� ������� �� ������ ��
        public void OnGet()
        {
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .ToList();

            // �������� ���� ������� 
            NamePurpose = new SelectList(_context.PurposeTravels.AsNoTracking().Select(c => c.NamePurpTravel));

            // �������� ������
            NameCountry = new SelectList(_context.Countries.AsNoTracking().Select(c => c.NameCountry));
        } // OnGet

        // ���������� ����� �� ������� submit
        // �������� ������ �� ��������������
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var travel = await _context.Travels.FindAsync(id);

            if (travel != null)
            {
                _context.Remove(travel);
                await _context.SaveChangesAsync();
            } // if

            return RedirectToPage();
        } // OnPostDeleteAsync

    } // class TravelsAllModel
}
