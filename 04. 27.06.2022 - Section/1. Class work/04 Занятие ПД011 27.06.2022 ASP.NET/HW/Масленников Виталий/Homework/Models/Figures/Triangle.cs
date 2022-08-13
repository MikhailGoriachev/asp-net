using Homework.Infrastructure;
namespace Homework.Models.Figures;

public sealed class Triangle : IFigure
{
	private double _a;
	private double _b;
	private double _c;

	public string Name => "Треугольник";
	public string? Image { get; set; }

	public double SideA
	{
		get => _a;
		//set => _a = value > 0
		//	? value
		//	: throw new InvalidDataException($"Недопустимое значение: {value}");
	}
	public double SideB
	{
		get => _b;
		//set => _b = value > 0
		//	? value
		//	: throw new InvalidDataException($"Недопустимое значение: {value}");
	}

	public double SideC
	{
		get => _c;
		//set => _c = value > 0
		//	? value
		//	: throw new InvalidDataException($"Недопустимое значение: {value}");
	}

	public (double A, double B, double C) Sides
	{
		get => (SideA, SideB, SideC);
		set
		{
			if (!IsTriangle(value.A, value.B, value.C))
				throw new ArgumentException(
					"Некорректные параметры для сторон треугольника");

			(_a, _b, _c) = value;
		}
	}

	public Triangle(double sideA, double sideB, double sideC, string? image = "triangle.png")
	{
		Sides = (sideA, sideB, sideC);
		Image = image;
	}
	
	public double Area
	{
		get {
			double p = SideA + SideB + SideC / 2;
			return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
		}
	}

	public double Perimeter => SideA + SideB + SideC;

	public static bool IsTriangle(double a, double b, double c) =>
		a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a;
	public string ToTableRow(int row, string path)
	{
		return $"<tr>" +
		       $"<td class=\"align-center\">{row}</td>" +
		       $"<td><img class=\"img-200\" src=\"{path}/{Image}\" alt=\"Фигура\"/></td>" +
		       $"<td class=\"align-left\">{Name}</td>" +
		       $"<td class=\"align-left\"><div>a = {SideA:F3}</div> <div>b = {SideB:F3}</div> <div>c = {SideC:F3}</div></td>" +
		       $"<td>{Perimeter:F3}</td>" +
		       $"<td>{Area:F3}</td>" +
		       $"</tr>";
	}

	public static IFigure Generate(double min = 1, double max = 10)
	{
		double a, b, c;	
		do
		{
			a = Utilities.GenerateDouble(min, max);
			b = Utilities.GenerateDouble(min, max);
			c = Utilities.GenerateDouble(min, max);
		} while (!Triangle.IsTriangle(a, b, c));

		return new Triangle(a, b, c);
	}
}