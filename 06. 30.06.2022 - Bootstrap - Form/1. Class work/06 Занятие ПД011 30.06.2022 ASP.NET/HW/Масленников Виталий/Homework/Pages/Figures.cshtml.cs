using System.Diagnostics;
using System.Resources;
using Homework.Models;
using Homework.Models.Figures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages;

public class FiguresModel : PageModel
{
	public string ImageFolder => Url.Content($"~/images/figures");

	// Объект класса с коллекцией фигур
	public static FiguresCollection FiguresCollection = new();

	// Фигуры для отображения
	public List<IFigure>? DisplayFigures = FiguresCollection.Figures;

	public void OnGetFilter(string sort, string? figure = null, string order = "Asc")
	{
		if (figure != null)
			DisplayFigures = FiguresCollection.Figures!.Where(f => f.Name == figure).ToList();

		if (order == "Reverse")
			DisplayFigures!.Reverse();
		else
			DisplayFigures = FiguresCollection.GetOrdered(DisplayFigures, sort, order);
	}

}