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

	// Значения для выпадающего списка
	public List<SelectListItem> Types = new() {
		new SelectListItem("Квадрат", "Квадрат"),
		new SelectListItem("Прямоугольник", "Прямоугольник"),
		new SelectListItem("Треугольник", "Треугольник"),
	};

	// Объект класса с коллекцией фигур
	public static FiguresCollection FiguresCollection = new();

	// Фигуры для отображения
	public List<IFigure>? DisplayFigures = FiguresCollection.Figures;

	// Объект для ввода данных новой фигуры
	[BindProperty] public FigureInfo FigureInput { get; set; } = new("Квадрат", 1, 1, 1);

	// Строка для сообщений
	public string? Message;


	public void OnGetFilter(string sort, string? figure = null, string order = "Asc")
	{
		if (figure != null)
			DisplayFigures = FiguresCollection.Figures!.Where(f => f.Name == figure).ToList();

		if (order == "Reverse")
			DisplayFigures!.Reverse();
		else
			DisplayFigures = FiguresCollection.GetOrdered(DisplayFigures, sort, order);
	}
	
	public void OnPostAdd()
	{
		if (FigureInput.Type == "Треугольник" && !Triangle.IsTriangle(FigureInput.A, FigureInput.B, FigureInput.C))
		{
			Message = "Невозможно создать треугольник с введенными параметрами.";
			return;
		}

		IFigure figure = (FigureInput.Type) switch
		{
			"Квадрат" => new Square(FigureInput.A),
			"Прямоугольник" => new Rectangle(FigureInput.A, FigureInput.B),
			"Треугольник" => new Triangle(FigureInput.A, FigureInput.B, FigureInput.C),
			_ => throw new ArgumentOutOfRangeException()
		};

		FiguresCollection.Add(figure);
		FiguresCollection.Serialize();
	}

	public void OnPostAddRand()
	{
		FiguresCollection.Add();
		FiguresCollection.Serialize();
	}

	// Для установки стартовых состояний элементов ввода
	public string? StateForInputB() => FigureInput.Type is "Прямоугольник" or "Треугольник" ? null : "disabled";
	public string? StateForInputC() => FigureInput.Type == "Треугольник" ? null : "disabled";

	// Определение типа данных для ввода параметров новой фигуры
	public record FigureInfo(string Type, double A, double B, double C);

}