using Homework.Infrastructure;
using Homework.Models.Applicances;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages;

public class AppliancesListModel : PageModel
{
	public string ImageFolder => Url.Content("~/images/appliances");


	public List<Appliance> Appliances = new (new[]
	{
		new Appliance("Аэрогриль", "ART001", "Аэрогриль", 3200, 2, "air_fryer.jpeg"),
		new Appliance("Шашлычница", "ART002", "Шашлычница", 6100, 5, "brazier.jpeg"),
		new Appliance("Хлебопечка", "ART003", "Хлебопечка", 3400, 4, "breadmaker.jpeg"),
		new Appliance("Пылесос", "ART004", "Пылесос", 6100, 6, "cleaner.jpeg"),
		new Appliance("Кухонная вытяжка", "ART005", "Вытяжка", 2200, 2, "cooker_hood.jpeg"),
		new Appliance("Фритюрница", "ART006", "Фритюрница", 1540, 1, "fryer.jpeg"),
		new Appliance("Утюг", "ART007", "Утюг", 700, 8, "iron.jpeg"),
		new Appliance("Микроволновая печь", "ART008", "Печь", 11100, 4, "microwave_oven.jpeg"),
		new Appliance("Мини печь", "ART009", "Печь", 6000, 2, "mini_oven.jpeg"),
		new Appliance("Мультиварка", "ART010", "Мультиварка", 7700, 5, "multicooker.jpeg"),
		new Appliance("Плита кухонная", "ART011", "Плита", 16400, 1, "plate.jpeg"),
		new Appliance("Холодильник", "ART012", "Холодильник", 23800, 6, "refrigerator.jpeg"),
		new Appliance("Пароварка", "ART013", "Пароварка", 5500, 2, "steamer.jpeg"),
		new Appliance("Стиральная машина", "ART014", "Стиральная машина", 23100, 8, "washing_machine.jpeg"),
	});

	public void OnGet()
	{
	}

	public void OnGetOrderBy(string prop, string order = "Asc")
	{
		Appliances = order == "Asc" 
			? Appliances.AsQueryable().OrderBy(prop).ToList() 
			: Appliances.AsQueryable().OrderByDescending(prop).ToList();
	}


	public void OnPost(int priceFrom, int priceTo)
	{
		if (priceTo == 0)
			return;

		Appliances = Appliances.Where(item => item.Price >= priceFrom && item.Price <= priceTo).ToList();
	}

}