using System.Collections.Generic;
using System.Linq;
using Homework.Infrastructure;
using Homework.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages
{
	public class AppliancesListModel : PageModel
	{
		public string ImageFolder => Url.Content(@"~/images/appliances");

		// Класс, хранящий коллекцию бытовой техники
		public static Company Company { get; } = new();

		// Отображаемые данные
		public IEnumerable<Product>? DisplayProducts = Company.Products;

		// Значения для выпадающего списка
		public SelectList Types = new(Company.Types, nameof(Company.TypeInfo.Type), nameof(Company.TypeInfo.Type));

		[BindProperty] // Объект для ввода данных новой техники
		public Product Product { get; set; } = new("Наименование", "АРТ00", "Аэрогриль", 1000, 1, "air_fryer.jpeg");


		public void OnGetOrderBy(string prop, string order = "Asc") =>
			DisplayProducts = Company.GetOrdered(prop, order);


		public void OnPostAdd()
		{
			Product.Image = Company.Types.FirstOrDefault(t => t.Type == Product.Type)?.Image;

			Company.Add(Product);
			Company.Serialize();
		}
	}
}