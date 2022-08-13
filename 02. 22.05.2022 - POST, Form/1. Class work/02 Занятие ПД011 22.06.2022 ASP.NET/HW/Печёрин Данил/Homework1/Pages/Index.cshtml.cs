using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Homework1.Models;

namespace Homework1.Pages
{
    public class IndexModel : PageModel
    {
        public Book Sample { get; set; } = new Book();
        public void OnGet()
        {
        }
    }
}
