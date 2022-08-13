using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    [IgnoreAntiforgeryToken]
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
        }

        // обработчик запроса POST
        public void OnPost()
        {
            Book = new Book(Request.Form["author"], Request.Form["title"], int.Parse(Request.Form["year"]), "book.png");
        }
    }
}
