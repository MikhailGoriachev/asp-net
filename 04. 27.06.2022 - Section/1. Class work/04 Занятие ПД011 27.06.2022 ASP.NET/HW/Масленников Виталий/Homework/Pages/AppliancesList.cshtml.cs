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
		new Appliance("���������", "ART001", "���������", 3200, 2, "air_fryer.jpeg"),
		new Appliance("����������", "ART002", "����������", 6100, 5, "brazier.jpeg"),
		new Appliance("����������", "ART003", "����������", 3400, 4, "breadmaker.jpeg"),
		new Appliance("�������", "ART004", "�������", 6100, 6, "cleaner.jpeg"),
		new Appliance("�������� �������", "ART005", "�������", 2200, 2, "cooker_hood.jpeg"),
		new Appliance("����������", "ART006", "����������", 1540, 1, "fryer.jpeg"),
		new Appliance("����", "ART007", "����", 700, 8, "iron.jpeg"),
		new Appliance("������������� ����", "ART008", "����", 11100, 4, "microwave_oven.jpeg"),
		new Appliance("���� ����", "ART009", "����", 6000, 2, "mini_oven.jpeg"),
		new Appliance("�����������", "ART010", "�����������", 7700, 5, "multicooker.jpeg"),
		new Appliance("����� ��������", "ART011", "�����", 16400, 1, "plate.jpeg"),
		new Appliance("�����������", "ART012", "�����������", 23800, 6, "refrigerator.jpeg"),
		new Appliance("���������", "ART013", "���������", 5500, 2, "steamer.jpeg"),
		new Appliance("���������� ������", "ART014", "���������� ������", 23100, 8, "washing_machine.jpeg"),
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