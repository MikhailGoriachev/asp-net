using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages
{
    public class SailingFleetModel : PageModel
    {
	    public List<Fleet>? Fleets { get; set; }

        public void OnGet()
        {
	        Fleets = new List<Fleet>(new []
	        {
                new Fleet
                {
                    Type = "линейный корабль", Name = "Виктория", Year = 1765, Length = 63.34,
                    Width = 15.8, Displacement = 3500, Image = "victory.jpg"
                }, 
                new Fleet
                {
                    Type = "чайный клиппер", Name = "Катти Сарк", Year = 1869, Length = 85.4,
                    Width = 11, Displacement = 2133.779, Image = "сutty_sark.jpg"
                }, 
                new Fleet
                {
                    Type = "барк", Name = "Крузенштерн", Year = 1926, Length = 114.5,
                    Width = 14, Displacement = 5805, Image = "krusenstern.jpg"
                }, 
	        });
        }
    }
}
