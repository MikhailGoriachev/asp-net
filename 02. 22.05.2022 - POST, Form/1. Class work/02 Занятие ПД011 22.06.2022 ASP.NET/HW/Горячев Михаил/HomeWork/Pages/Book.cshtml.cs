using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    public class BookModel : PageModel
    {
        // книга
        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { _book = value; }
        }


        // обработчик запроса GET
        public void OnGet()
        {
            // создание книги
            Book = Book ?? new Book("Бриггс Дж.", "Python для детей. Самоучитель по программированию", 2017, "book_view.jpeg");
        }
    }
}
