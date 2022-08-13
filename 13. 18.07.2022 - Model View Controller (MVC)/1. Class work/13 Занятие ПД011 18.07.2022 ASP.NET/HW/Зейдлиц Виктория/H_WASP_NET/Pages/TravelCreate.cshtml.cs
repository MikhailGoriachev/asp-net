using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]
    public class TravelCreateModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ���� ��� �������� � ��������� �����
        [BindProperty] public Travel? Travel { get; set; }

        // �������� ������� ��� ����������� ������
        public SelectList Countries { get; private set; } = null!;
        public SelectList NamePurpose { get; private set; } = null!;


        // ����������� � ���������� ����������� ��� ���������
        // ������� � ���� ������
        public TravelCreateModel(ApplicationContext db)
        {
            _context = db;
        } // TravelCreateModel


        // ������ ������� �������� � ������ �����
        public void OnGet()
        {
            // �������� ������ �������� ��� ������������ ����������� ������
            Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "Id", "NameCountry");

            NamePurpose = new SelectList(_context.PurposeTravels.AsNoTracking().ToList(), "Id", "NamePurpTravel");
        } // OnGet


        // ��������� ����� ����� � ������� ���������� ������������
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Travels.Add(Travel);
            await _context.SaveChangesAsync();

            // ��������� �� �������� ����������� ���� ������� �������
            return RedirectToPage("TravelsAll");
        } // OnPostAsync

    } // class TravelCreateModel
}
