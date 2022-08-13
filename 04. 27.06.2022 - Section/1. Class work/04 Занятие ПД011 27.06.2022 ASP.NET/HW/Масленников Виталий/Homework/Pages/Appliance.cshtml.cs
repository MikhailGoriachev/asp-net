using Homework.Models.Applicances;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages;

public class ApplianceModel : PageModel
{
	[BindProperty] 
	public Appliance? Appliance { get; set; } = new ();

	public void OnGet()
	{
	}

	public void OnPost()
	{
	}
}