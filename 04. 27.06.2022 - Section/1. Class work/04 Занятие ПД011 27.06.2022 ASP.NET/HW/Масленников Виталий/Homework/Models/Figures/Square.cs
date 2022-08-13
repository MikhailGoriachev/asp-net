using Homework.Infrastructure;

namespace Homework.Models.Figures;

public sealed class Square : IFigure
{
	private double _a;

	public string Name => "Квадрат";
	public string? Image { get; set; }

	public double SideA
	{
		get => _a;
		set => _a = value > 0
			? value
			: throw new InvalidDataException($"Недопустимое значение: {value}");
	}

	public Square(double sideA, string? image = "square.png")
	{
		SideA = sideA;
		Image = image;
	}

	public double Area => SideA * SideA;
	public double Perimeter => 4 * SideA;
	public string ToTableRow(int row, string path)
	{
		return $"<tr>" +
		       $"<td class=\"align-center\">{row}</td>" +
		       $"<td><img class=\"img-200\" src=\"{path}/{Image}\" alt=\"Фигура\"/></td>" +
		       $"<td class=\"align-left\">{Name}</td>" +
		       $"<td class=\"align-left\">a = {SideA:F3}</td>" +
		       $"<td>{Perimeter:F3}</td>" +
		       $"<td>{Area:F3}</td>" +
		       $"</tr>";
	}

	public static IFigure Generate(double min = 1, double max = 10)
	{
		return new Square(Utilities.GenerateDouble(min, max));
	}
}