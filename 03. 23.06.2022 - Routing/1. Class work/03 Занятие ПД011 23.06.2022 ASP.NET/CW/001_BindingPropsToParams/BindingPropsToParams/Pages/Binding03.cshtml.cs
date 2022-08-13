using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingPropsToParams.Pages
{
    // привязка свойств к GET-параметрам

    [IgnoreAntiforgeryToken]
    public class Binding03Model : PageModel
    {
        // для получения данных из GET-запроса: SupportsGet = true
        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        // для получения данных из GET-запроса: SupportsGet = true
        [BindProperty(SupportsGet = true)]
        public int Salary { get; set; } 


        public void OnGet()
        {
        }
    }
}
