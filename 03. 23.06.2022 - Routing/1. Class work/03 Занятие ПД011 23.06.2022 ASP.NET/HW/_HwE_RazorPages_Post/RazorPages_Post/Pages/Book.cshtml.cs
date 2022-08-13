using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RazorPages_Post.Models;
using RazorPages_Post.Infrastructure;

namespace RazorPages_Post.Pages
{
    [IgnoreAntiforgeryToken]
    public class BookModel : PageModel
    {
        public Book BookData {
            get; private set;
        }

        // �� GET-������� ��������� ������ ������ Book � ������ �� ���������
        public void OnGet() {
            BookData = new Book();

            // ��������� ��� ����� ����������� �������
            BookData.Cover = "/images/task01/cover" + Utils.GetRandom(1, 5) + ".jpg";
        } // OnGet
        

        // ��������� ������ ����� �� ����� �� ������� onPost, �������
        // �������� ����� �� ��������
        public void OnPost() {
            // ��� ��������� ������������� - ������ �� �����
            var form = Request.Form;

            // ������� ������, ��������� ��� ��������
            BookData = new Book  {
                Author = form["author"],
                Title = form["title"],
                Year = int.Parse(form["year"]),
                Price = int.Parse(form["price"]),
                Cover = "/images/task01/cover" + Utils.GetRandom(1, 5) + ".jpg"
            };

            // � ������������� ������ ������� ������
        } // OnPost
    } // class BookModel
}
