using Homework.Infrastructure;

namespace Homework.Models.Figures;

public sealed class Rectangle :  IFigure
{
	private double _a;
	private double _b;

	public string Name => "Прямоугольник";
	public string? Image { get; set; }

	public double SideA
	{
		get => _a;
		set => _a = value > 0
			? value
			: throw new InvalidDataException($"Недопустимое значение: {value}");
	}
	public double SideB
	{
		get => _b;
		set => _b = value > 0
			? value
			: throw new InvalidDataException($"Недопустимое значение: {value}");
	}

	public Rectangle(double sideA, double sideB, string? image = "rectangle.png")
	{
		SideA = sideA;
		SideB = sideB;
		Image = image;
	}
	
	public double Area => SideB * SideA;
	public double Perimeter => 2d * (SideA + SideA);
	public string ToTableRow(int row, string path)
	{
		return $"<tr>" +
		       $"<td class=\"align-center\">{row}</td>" +
		       $"<td><img class=\"img-200\" src=\"{path}/{Image}\" alt=\"Фигура\"/></td>" +
		       $"<td class=\"align-left\">{Name}</td>" +
		       $"<td class=\"align-left\"><div>a = {SideA:F3}</div> <div>b = {SideB:F3}</div></td>" +
		       $"<td>{Perimeter:F3}</td>" +
		       $"<td>{Area:F3}</td>" +
		       $"</tr>";
	}


	public static IFigure Generate(double min = 1, double max = 10)
	{
		return new Rectangle(Utilities.GenerateDouble(min, max), Utilities.GenerateDouble(min, max));
	}
}