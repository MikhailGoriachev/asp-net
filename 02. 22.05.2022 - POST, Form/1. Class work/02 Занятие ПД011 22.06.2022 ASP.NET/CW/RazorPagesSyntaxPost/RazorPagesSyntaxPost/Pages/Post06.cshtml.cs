using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    // ���� � ����� �������
    public class Post06Model : PageModel
    {
        // ���������, ��������� � ��������� �����
        public string Header { get; private set; } = "";

        // ������� ������ ��� ����� � ������
        public Dictionary<string, string> Countries { get; set; } = new();


        // ��� ������� �������� ������ ������ ������� ���� � ����� � ��������� �����
        // �������, ����� ������ ������ �� �������������
        public void OnGet() {
            Header = $"{DateTime.Now:G}";
        } // OnGet

        
        // ��������� ����� �� ������� POST
        public void OnPost(Dictionary<string, string> items) {
            Header = $"{DateTime.Now:G}";
            // ����� ������� ������
            Countries = items;
        } // OnPost
    } //  class Post06Model 
}
