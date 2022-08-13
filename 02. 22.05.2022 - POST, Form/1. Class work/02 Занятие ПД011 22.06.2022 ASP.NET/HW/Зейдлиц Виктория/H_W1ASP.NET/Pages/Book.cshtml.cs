using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace H_W1ASP.NET.Pages
{
    public class Page01Model : PageModel
    {
        public string NameBook = "Python для детей. Самоучитель по программированию";

        public string Author = "Бриггс Дж.";

        public int Year = 2017;


        public void OnGet()
        {
        }
    }
}
