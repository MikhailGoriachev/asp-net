using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingPropsToParams.Pages
{
    [IgnoreAntiforgeryToken]
    public class Binding04Model : PageModel
    {
        // переименование параметров - Name = имяПараметра
        [BindProperty(SupportsGet = true, Name = "eman")]
        public string? Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Age { get; set; }

        // еще один пример переименования параметра
        [BindProperty(SupportsGet = true, Name= "earn")]
        public int Salary { get; set; }

        public void OnGet()
        {
        }
    }
}
