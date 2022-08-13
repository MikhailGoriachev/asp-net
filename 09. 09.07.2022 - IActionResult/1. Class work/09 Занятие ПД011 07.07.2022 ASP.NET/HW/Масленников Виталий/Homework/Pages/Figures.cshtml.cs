using System;
using System.Collections.Generic;
using System.Linq;
using Homework.Models.Figures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages
{
	public class FiguresModel : PageModel
	{
		public string ImageFolder => Url.Content(@"~/images/figures");

		// Значения для списка фигур
		public SelectList Types { get; } = new(new List<string> { "Квадрат", "Прямоугольник", "Треугольник" });

		// Строка для сообщений
		public string? Message;
		public string? ErrMsg;

		// Объекты для ввода данных новой фигуры
		[BindProperty] public string? FigureType { get; set; }
		[BindProperty] public FigureSides? FigureInput { get; set; }

		// Объект класса с коллекцией фигур
		private static FiguresCollection _figuresCollection = new();

		// Фигуры для отображения
		public IEnumerable<IFigure>? DisplayFigures = _figuresCollection.Figures;



		public void OnGetFilter(string sort, string? figure = null, string order = "Asc")
		{
			if (figure is not null)
				DisplayFigures = _figuresCollection.Figures!.Where(f => f.Name == figure);

			DisplayFigures = order == "Reverse"
				? DisplayFigures!.Reverse()
				: FiguresCollection.GetOrdered(DisplayFigures!, sort, order);
		}

		public void OnPostAddFigure()
		{
			try
			{
				IFigure figure = FigureType switch
				{
					"Квадрат" => new Square(FigureInput!.A),
					"Прямоугольник" => new Rectangle(FigureInput!.A, FigureInput.B),
					"Треугольник" => new Triangle(FigureInput!.A, FigureInput.B, FigureInput.C),
					_ => throw new ArgumentOutOfRangeException()
				};

				_figuresCollection.Add(figure);
				_figuresCollection.Serialize();

				Message = $"{FigureType} добавлен в коллекцию";
			}
			catch (Exception ex)
			{
				ErrMsg = ex.Message;
			}
		}

		// Количество фигур по типу
		public int AmountOf(string type) => _figuresCollection.AmountOf(type);

		// Тип данных для ввода параметров новой фигуры
		public record FigureSides(double A, double B, double C);
	}
}