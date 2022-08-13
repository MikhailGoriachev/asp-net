using Homework.Infrastructure;
using Homework.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages;

public class AppliancesListModel : PageModel
{
	public string ImageFolder => Url.Content("~/images/appliances");

	// Класс, хранящий коллекцию бытовой техники
	public static Company Company { get; } = new();

	// Отображаемые данные
	public List<Product>? DisplayProducts = Company.Products;

	public void OnGetOrderBy(string prop, string order = "Asc") => 
		DisplayProducts = Company.GetOrdered(prop, order);

}