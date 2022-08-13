using System.Diagnostics;
using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages
{
    public class SailingFleetModel : PageModel
    {
	    public List<Ship>? Fleet { get; set; } = new List<Ship>();
	    

        public void OnGet()
        {
        }

        public void OnPost(Ship[] ships)
        {
	        Fleet = new List<Ship>(ships);
        }

    }
}
