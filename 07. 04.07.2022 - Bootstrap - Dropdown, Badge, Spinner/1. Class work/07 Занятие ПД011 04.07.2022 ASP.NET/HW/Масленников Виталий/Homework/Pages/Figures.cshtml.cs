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

	// �������� ��� ������ �����
	public List<SelectListItem> Types = new()
	{
		new SelectListItem("�������", "�������"),
		new SelectListItem("�������������", "�������������"),
		new SelectListItem("�����������", "�����������"),
	};  
	
	// ������ ��� ���������
	public string? Message;

	// ������� ��� ����� ������ ����� ������
	[BindProperty] public string? FigureType { get; set; }
	[BindProperty] public FigureSides FigureInput { get; set; } = new( 1, 1, 1);

	// ������ ������ � ���������� �����
	public static FiguresCollection FiguresCollection = new();

	// ������ ��� �����������
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
		if (FigureType == "�����������" && !Triangle.IsTriangle(FigureInput.A, FigureInput.B, FigureInput.C))
		{
			Message = "���������� ������� ����������� � ���������� �����������.";
			return;
		}

		IFigure figure = (FigureType) switch
		{
			"�������" => new Square(FigureInput.A),
			"�������������" => new Rectangle(FigureInput.A, FigureInput.B),
			"�����������" => new Triangle(FigureInput.A, FigureInput.B, FigureInput.C),
			_ => throw new ArgumentOutOfRangeException()
		};

		FiguresCollection.Add(figure);
		FiguresCollection.Serialize();

		Message = $"{FigureType} �������� � ���������";
	}


	// ��� ������ ��� ����� ���������� ����� ������
	public record FigureSides(double A, double B, double C);
}