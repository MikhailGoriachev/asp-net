using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorSyntaxContinue.Pages
{
    /*
     * ����� ������ ������ ��� ��������� ������������ ���� - ����� ������
     * Razor ��� ��������� ���������� ���� ���������� ����������� ����� -
     * AntiforgeryToken.
     * ����� �� ����������, ��� ��� �������������. ������ �� ����� ���������
     * ��������� ����� � ������� ����� ������ � ������� ��������
     * [IgnoreAntiforgeryToken], ������� ����������� � ������ ������ (�����
     * ����� ��������� ��������� � ����������).
     *
     */
    [IgnoreAntiforgeryToken]
    public class Post01 : PageModel
    {
        public string Message { get; private set; } = "";
        public string UserName { get; private set; } = "";

        // ���������� GET-������� �������� �� �������
        // http://localhost:5283/Post01
        // �� ����� ������� ����������� � ������������� �������� HTML + CSS + ...
        public void OnGet() {
            Message = "������� ���� ���";
            UserName = @"-- """" --";
        } // OnGet


        // ���������� POST-������� - ���������� ����������
        // �� ����� ������
        // ��������� ������ -- ��������� POST-������� (���� �����)
        // ��� ���������� �������-����������
        public void OnPost(string username) {
            Message = "������� ���� ���";
            UserName = $"{username}";
        } // OnPost
    }
}
