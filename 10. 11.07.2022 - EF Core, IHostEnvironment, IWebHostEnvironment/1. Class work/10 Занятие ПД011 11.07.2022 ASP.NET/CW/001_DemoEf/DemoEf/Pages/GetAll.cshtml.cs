using DemoEf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoEf.Pages
{
    // ������������� ������� �� � ������������ �������� ������,
    // �������� � �������������� ������ 
    public class GetAllModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<User>? Users { get; private set; }


        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public GetAllModel(ApplicationContext db) {
            _context = db;
        } // GetAllModel

        // �� GET-������ ������ ������� �������� � ����������
        // ���������� ������� �� ������� ��
        public void OnGet() {
            Users = _context.Users.AsNoTracking().ToList();
        } // OnGet

        // ���������� ����� �� ������� submit
        // �������� ������ �� ��������������
        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            var user = await _context.Users.FindAsync(id);
            if (user != null) {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            } // if

            return RedirectToPage();
        } // OnPostDeleteAsync
    } //  class GetAllModel
}
