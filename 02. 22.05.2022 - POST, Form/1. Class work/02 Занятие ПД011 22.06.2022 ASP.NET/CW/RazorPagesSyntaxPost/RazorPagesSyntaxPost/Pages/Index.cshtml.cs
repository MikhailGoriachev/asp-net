using EmptyProjectRazorPagesLayout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmptyProjectRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        // ������ ������������� �������� ������
        public string Message { get; private set; } = "";

        //public List<Driver> Drivers = new List<Driver>(new[] {
        //    new Driver("������", 34),
        //    new Driver("������", 46),
        //    new Driver("���������", 29),
        //});

        // ���������� Get-������� 
        public void OnGet() {
            Message = "����� ����������� � codebehind";
            // throw new InvalidDataException("��������....");
        } // OnGet
    }
}
