using Homework.Models.Figures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Pages;

public class FiguresModel : PageModel
{
	public string ImageFolder => Url.Content(@"~/images/figures");

	// �������� ��� ������ �����
	public SelectList Types { get; } = new (new List<string> { "�������", "�������������", "�����������" });

	// ������ ��� ���������
	public string? Message;
	public string? ErrMsg;

	// ������� ��� ����� ������ ����� ������
	[BindProperty] public string? FigureType { get; set; }
	[BindProperty] public FigureSides? FigureInput { get; set; }

	// ������ ������ � ���������� �����
	private static FiguresCollection _figuresCollection = new();

	// ������ ��� �����������
	public IEnumerable<IFigure>? DisplayFigures = _figuresCollection.Figures;



	public void OnGetFilter(string sort, string? figure = null, string order = "Asc")
	{
		if (figure is not null)
			DisplayFigures = _figuresCollection.Figures!.Where(f => f.Name == figure);

		DisplayFigures = order == "Reverse" ?
			DisplayFigures!.Reverse() : 
			FiguresCollection.GetOrdered(DisplayFigures!, sort, order);
	}

	public void OnPostAddFigure()
	{
		try
		{
			IFigure figure = FigureType switch
			{
				"�������" => new Square(FigureInput!.A),
				"�������������" => new Rectangle(FigureInput!.A, FigureInput.B),
				"�����������" => new Triangle(FigureInput!.A, FigureInput.B, FigureInput.C),
				_ => throw new ArgumentOutOfRangeException()
			};

			_figuresCollection.Add(figure);
			_figuresCollection.Serialize();

			Message = $"{FigureType} �������� � ���������";
		}
		catch (Exception ex)
		{
			ErrMsg = ex.Message;
		}
	}

	// ���������� ����� �� ����
	public int AmountOf(string type) => _figuresCollection.AmountOf(type);

	// ��� ������ ��� ����� ���������� ����� ������
	public record FigureSides(double A, double B, double C);
}