using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Home_Work.Models;

namespace Home_Work.Pages
{
    [IgnoreAntiforgeryToken]
    public class BookModel : PageModel
    {
        private Book _book;
        public Book Book { 
            get { return _book; } 
            set {

                if (value == null)
                    return;
                _book = value; 
            } 
        }

        public string Message { get; set; }
        public void OnGet()
        {
            Book = new Book();
            Message = "Книга сформирована!";
        }

        public void OnPost(Book book)
        {
            Book = book;
            Message = "Книга изменена!";
        }
    }
}
