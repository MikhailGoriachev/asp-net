using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]

    public class TravelEditModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        [BindProperty] public Travel? Travel { get; set; }

        // �������� ������� ��� ����������� ������
        public SelectList NameCountry { get; private set; } = null!;

        public SelectList NamePurpose { get; private set; } = null!;

        // ����������� � ���������� �����������, ������ � ��
        public TravelEditModel(ApplicationContext db)
        {
            _context = db;
        } // TravelEditModel


        // �� GET-������� ����� ������ � �������
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Travel = await _context.Travels.FindAsync(id);

            // �������� ���� ������� 
            NamePurpose = new SelectList(_context.PurposeTravels.AsNoTracking().Select(c => c.NamePurpTravel));

            // �������� ������
            NameCountry = new SelectList(_context.Countries.AsNoTracking().Select(c => c.NameCountry));
            return Page();
        } // OnGetAsync


        // ��������� ������, ���������� �� �����
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Travels.Update(Travel);
            await _context.SaveChangesAsync();

            // ������� �� �������� ����������� ���� �������
            return RedirectToPage("TravelsAll");
        } // OnPostAsync

    } // class TravelEditModel
}
