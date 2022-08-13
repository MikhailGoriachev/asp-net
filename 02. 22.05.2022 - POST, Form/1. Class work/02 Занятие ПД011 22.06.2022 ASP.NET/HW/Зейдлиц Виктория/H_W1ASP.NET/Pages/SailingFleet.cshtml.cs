using H_W1ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace H_W1ASP.NET.Pages
{
    public class SailingFleetModel : PageModel
    {
        public List<Ship> Ships = new List<Ship>(new[]
        {
            new Ship("Виктория", 69.34, 15.8, 3, 1765),
            new Ship("Катти Сарк", 85.4, 10.97, 6, 1869),
            new Ship("Крузенште́рн", 114.5, 14, 7, 1926)
        });


        public void OnGet()
        {
        }
    }
}
