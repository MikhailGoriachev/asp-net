using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages
{
    public class BookModel : PageModel
    {
        public Book? Book { get; set; }
        public void OnGet()
        {
            Book = new Book
            {
	            Title = "«Python для детей. Самоучитель по программированию»",
	            Author = "Бриггс Дж.",
	            Year = 2017,
	            Image = "book.jpeg"
            };
        }
    }
}
