using EmptyProjectRazorPagesLayout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    // ��� POST-���������
    [IgnoreAntiforgeryToken]
    public class Post02Model : PageModel
    {
        public string Message { get; private set; } = "";

        // ������ �������� � ������
        public void OnGet() {
            Message = "������� ���� ������";
        }

        // ��������� POST-�������
        // public void OnPost(string name, int age) {
        public void OnPost(Person person) {
            // Message = $"�� �����: {name}, {age}";
            Message = $"�� �����: {person.Name}, {person.Age}";
        } // OnPost
    }

    public record class Person(string Name, int Age);
}
