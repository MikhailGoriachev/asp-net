using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoutingIntro.Pages
{
    // 
    public class Page01Model : PageModel
    {
        public object? Id { get; set; }

        public void OnGet() {
            Id = RouteData.Values["id"];
        }
    }
}
