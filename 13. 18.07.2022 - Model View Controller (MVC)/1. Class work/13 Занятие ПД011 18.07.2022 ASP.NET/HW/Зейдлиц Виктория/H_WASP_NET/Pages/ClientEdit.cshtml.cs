using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace H_WASP_NET.Pages
{
    [IgnoreAntiforgeryToken]
    public class ClientEditModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        [BindProperty] public Client? Client { get; set; }


        // ����������� � ���������� �����������, ������ � ��
        public ClientEditModel(ApplicationContext db)
        {
            _context = db;
        } // ClientEditModel


        // �� GET-������� ����� ������ � �������, ��������� HTML-�������� ����� ������� 
        public async Task<IActionResult> OnGetAsync(int id)
        {

           Client = await _context.Clients.FindAsync(id);

            return Page();
        } // OnGetAsync


        // ��������� ������, ���������� �� �����
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Clients.Update(Client!);
            await _context.SaveChangesAsync();

            // ��������� �� �������� ����������� ���� ������� �������
            return RedirectToPage("ClientsAll");
        } // OnPostAsync
    }
}
