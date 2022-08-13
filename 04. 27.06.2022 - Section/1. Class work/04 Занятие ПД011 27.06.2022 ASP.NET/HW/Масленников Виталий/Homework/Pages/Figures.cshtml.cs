using Homework.Controllers;
using Homework.Models.Figures;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages;

public class FiguresModel : PageModel
{
	public string ImageFolder => Url.Content($"~/images/figures");

	public Dictionary<string, string> FigureTypes = new() {
        ["Square"] = "Квадрат",
        ["Rectangle"] = "Прямоугольник",
        ["Triangle"] = "Треугольник",
	};

	public static FiguresController FiguresController = new ();

	public List<IFigure>? DisplayFigures;

	public void OnGet()
	{
		DisplayFigures = FiguresController.Figures;
	}

	public void OnGetFilter(string sort, string? figure = null, string order = "Asc")
	{
		DisplayFigures = figure != null
			? FiguresController.Figures!.Where(f => f.Name == FigureTypes[figure]).ToList()
			: FiguresController.Figures;

		if (order == "Reverse")
			DisplayFigures!.Reverse();
		else
			DisplayFigures = FiguresController.GetOrdered(DisplayFigures, sort, order);
	}
	
	public void OnPostAdd()
	{
		FiguresController.Add();
		DisplayFigures = FiguresController.Figures;
	}

	public void OnPostGenerate()
	{
		FiguresController.Generate();
		DisplayFigures = FiguresController.Figures;
	}

}