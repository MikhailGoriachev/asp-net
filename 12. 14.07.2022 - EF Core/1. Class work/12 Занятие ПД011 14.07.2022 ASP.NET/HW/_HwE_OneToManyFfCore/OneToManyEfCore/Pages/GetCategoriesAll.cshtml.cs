using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToManyEfCore.Models;

namespace OneToManyEfCore.Pages
{
    // ������ � �������� ���������
    public class GetCategoriesAllModel : PageModel
    {
        // ������ �� ���� ������
        private ApplicationContext _context;

        // ������ �� ��������� ��������� - ������� �� ��
        public List<Category>? Categories { get; private set; }

        // ��������� ������� � �� ��� ������ ������������
        // � ���������� �����������
        public GetCategoriesAllModel(ApplicationContext db)
        {
            _context = db;
        } // GetCategoriesAllModel

        // �� GET-������ ������ ������� �������� � ����������
        // ���������� ������� �� ������� ��
        public void OnGet() {
            Categories = _context.Categories
                .AsNoTracking()
                .ToList();
        } // OnGet
    } // class GetCategoriesAllModel
}
