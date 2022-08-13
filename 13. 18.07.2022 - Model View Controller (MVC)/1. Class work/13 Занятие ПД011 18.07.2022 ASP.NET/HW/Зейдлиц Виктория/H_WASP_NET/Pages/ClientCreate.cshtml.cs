using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]
    public class ClientCreateModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ���� ��� �������� � ��������� �����
        [BindProperty] public Client? Client { get; set; }


        // ����������� � ���������� ����������� ��� ���������
        // ������� � �a�� ������
        public ClientCreateModel(ApplicationContext db)
        {
            _context = db;
        } // ClientCreateModel


        // ������ ������� �������� � ������ �����
        public void OnGet() { } // OnGet


        // ��������� ����� ����� � ������� ���������� �������
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Clients.Add(Client);
            await _context.SaveChangesAsync();


            // ��������� �� �������� ����������� ���� ������� �������
            return RedirectToPage("ClientsAll");
        } // OnPostAsync

    } // class ClientCreateModel
}
