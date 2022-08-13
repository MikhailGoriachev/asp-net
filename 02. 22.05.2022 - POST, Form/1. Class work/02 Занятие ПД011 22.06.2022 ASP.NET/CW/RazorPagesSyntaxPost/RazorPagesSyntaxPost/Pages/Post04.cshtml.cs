using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    public class Post04Model : PageModel
    {
        public Person[] People { get; private set; } = { };

        public void OnGet()
        {
        }

        // ����� ��������� ������� ��������
        public void OnPost(Person[] people)
        {
            People = people;
        }
    }
}
