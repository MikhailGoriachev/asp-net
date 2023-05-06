using Newtonsoft.Json;

namespace Homework.Models.Figures
{
	public sealed class Triangle : IFigure
	{
		[JsonProperty] private double _a;
		[JsonProperty] private double _b;
		[JsonProperty] private double _c;

		public string Name => "Треугольник";
		public string Image => "triangle.png";

		public int Id { get; set; }

		[JsonIgnore]
		public (double A, double B, double C) Sides
		{
			get => (_a, _b, _c);
			set
			{
				if (!IsTriangle(value.A, value.B, value.C))
					throw new ArgumentException(
						"Некорректные параметры для сторон треугольника");

				(_a, _b, _c) = value;
			}
		}

		public Triangle()
		{
		}

		public Triangle(int id, double sideA, double sideB, double sideC)
		{
			Sides = (sideA, sideB, sideC);
		}

		public static bool IsTriangle(double a, double b, double c) =>
			a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a;

		[JsonIgnore]
		public double Area
		{
			get
			{
				double p = _a + _b + _c / 2;
				return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));
			}
		}

		[JsonIgnore] public double Perimeter => _a + _b + _c;

		[JsonIgnore]
		public Dictionary<string, double> Parameters => new()
		{
			["a"] = _a,
			["b"] = _b,
			["c"] = _c,
		};
	}
}