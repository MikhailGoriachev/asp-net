using Homework.Infrastructure;
using Homework.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages;

public class AppliancesListModel : PageModel
{
	public string ImageFolder => Url.Content("~/images/appliances");

	// �����, �������� ��������� ������� �������
	public static Company Company { get; } = new();

	// ������������ ������
	public List<Product>? DisplayProducts = Company.Products;

	public void OnGetOrderBy(string prop, string order = "Asc") => 
		DisplayProducts = Company.GetOrdered(prop, order);

}