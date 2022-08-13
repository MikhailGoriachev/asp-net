using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoutingIntro.Pages.Shared
{
    public class Page02Model : PageModel
    {
        public int Id { get; set; }

        // параметр с именем id ищется в маршруте, в формах, в строке запроса
        public void OnGet(int id) {
            Id = id;
        }
    }
}
