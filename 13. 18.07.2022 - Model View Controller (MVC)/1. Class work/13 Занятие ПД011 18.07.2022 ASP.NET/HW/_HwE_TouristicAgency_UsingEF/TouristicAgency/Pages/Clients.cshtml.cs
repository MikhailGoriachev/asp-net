using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages
{
    // ������ � �������� �������
    public class ClientsModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<Client>? Clients { get; private set; }

        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public ClientsModel(ApplicationContext db)
        {
            _context = db;
        } // ClientsModel

        public void OnGet()
        {
            Clients = _context.Clients
                .AsNoTracking()
                .ToList();
        }
    }
}
