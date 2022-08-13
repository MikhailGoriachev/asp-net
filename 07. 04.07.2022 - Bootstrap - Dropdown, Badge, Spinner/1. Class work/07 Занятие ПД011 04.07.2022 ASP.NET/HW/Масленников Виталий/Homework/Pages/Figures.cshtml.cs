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

	// Значения для списка фигур
	public List<SelectListItem> Types = new()
	{
		new SelectListItem("Квадрат", "Квадрат"),
		new SelectListItem("Прямоугольник", "Прямоугольник"),
		new SelectListItem("Треугольник", "Треугольник"),
	};  
	
	// Строка для сообщений
	public string? Message;

	// Объекты для ввода данных новой фигуры
	[BindProperty] public string? FigureType { get; set; }
	[BindProperty] public FigureSides FigureInput { get; set; } = new( 1, 1, 1);

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


	public void OnPostAddFigure()
	{
		if (FigureType == "Треугольник" && !Triangle.IsTriangle(FigureInput.A, FigureInput.B, FigureInput.C))
		{
			Message = "Невозможно создать треугольник с введенными параметрами.";
			return;
		}

		IFigure figure = (FigureType) switch
		{
			"Квадрат" => new Square(FigureInput.A),
			"Прямоугольник" => new Rectangle(FigureInput.A, FigureInput.B),
			"Треугольник" => new Triangle(FigureInput.A, FigureInput.B, FigureInput.C),
			_ => throw new ArgumentOutOfRangeException()
		};

		FiguresCollection.Add(figure);
		FiguresCollection.Serialize();

		Message = $"{FigureType} добавлен в коллекцию";
	}


	// Тип данных для ввода параметров новой фигуры
	public record FigureSides(double A, double B, double C);
}