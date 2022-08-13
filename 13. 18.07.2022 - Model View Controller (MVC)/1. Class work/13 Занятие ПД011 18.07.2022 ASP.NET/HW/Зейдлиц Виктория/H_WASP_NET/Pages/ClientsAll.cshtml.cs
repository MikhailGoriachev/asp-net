using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{

    // ������ � �������� �������
    public class ClientsAllModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<Client>? Clients { get; private set; }

        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public ClientsAllModel(ApplicationContext db)
        {
            _context = db;
        } //  ClientsAllModel

        // �� GET-������� ������ ������� �������� � ����������
        // ���������� ������� �� ������� ��
        public void OnGet()
        {
            Clients = _context.Clients
                .AsNoTracking()
                .ToList();
        } // OnGet
    }
}
