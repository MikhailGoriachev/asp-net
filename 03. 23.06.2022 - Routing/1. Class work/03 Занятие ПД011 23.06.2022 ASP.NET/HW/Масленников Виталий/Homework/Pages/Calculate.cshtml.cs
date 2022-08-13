using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages
{
    public class CalculateModel : PageModel
    {
	    public double? z1;
	    public double? z2;
        
	    public void OnGet()
        {
        }

	    public void OnPost(double alpha, double beta)
	    {
		    z1 = (Math.Sin(alpha) + Math.Cos(2d * beta - alpha)) / (Math.Cos(alpha) - Math.Sin(2d * beta - alpha));
		    z2 = (1 + Math.Sin(2d * beta)) / Math.Cos(2d * beta);
	    }
    }
}
