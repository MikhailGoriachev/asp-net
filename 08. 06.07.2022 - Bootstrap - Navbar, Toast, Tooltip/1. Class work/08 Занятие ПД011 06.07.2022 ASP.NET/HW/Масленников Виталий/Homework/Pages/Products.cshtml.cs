using Homework.Infrastructure;
using Homework.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages;

public class AppliancesListModel : PageModel
{
	public string ImageFolder => Url.Content(@"~/images/appliances");

	// ?????, ???????? ????????? ??????? ???????
	public static Company Company { get; } = new();

	// ???????????? ??????
	public IEnumerable<Product>? DisplayProducts = Company.Products;

	// ???????? ??? ??????????? ??????
	public SelectList Types = new(Company.Types, nameof(Company.TypeInfo.Type), nameof(Company.TypeInfo.Type));

	[BindProperty] // ?????? ??? ????? ?????? ????? ???????
	public Product Product { get; set; } = new("????????????", "???00", "?????????", 1000, 1, "air_fryer.jpeg");


	public void OnGetOrderBy(string prop, string order = "Asc") => 
		DisplayProducts = Company.GetOrdered(prop, order);


	public void OnPostAdd()
	{
		Product.Image = Company.Types.FirstOrDefault(t => t.Type == Product.Type)?.Image;

		Company.Add(Product);
		Company.Serialize();
	}
}