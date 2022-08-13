using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Intro.Models;

namespace RazorPages_Intro.Pages
{
    public class BookModel : PageModel
    {
        private Book _book;
        public Book BookData {
            get => _book; private set => _book = value;
        }

        // �� GET-������� ��������� ������ ������ Book � ������ �� ���������
        public void OnGet() {
            _book = new Book();

            // ������������ ��� ����� ����������� �������
            _book.Cover = "/images/task01/" + _book.Cover;
        } // OnGet
    } // class BookModel
}
