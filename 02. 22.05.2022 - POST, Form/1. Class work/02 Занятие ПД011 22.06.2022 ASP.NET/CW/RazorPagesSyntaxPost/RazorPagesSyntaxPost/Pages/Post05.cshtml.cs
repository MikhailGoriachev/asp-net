using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    public class Post05Model : PageModel
    {
        public string Message { get; private set; } = "";

        // ������ �������� � ������
        public void OnGet()
        {
            Message = "������� ���� ������";
        }

        // ��������� POST-������� -��������� ������ �� ��������� �������
        public void OnPost() {
            Person person = new Person(
                this.Request.Form["name"],
                int.Parse(Request.Form["age"]));

            Message = $"�� �����: {person.Name}, {person.Age}";
        } // OnPost
    }
}
