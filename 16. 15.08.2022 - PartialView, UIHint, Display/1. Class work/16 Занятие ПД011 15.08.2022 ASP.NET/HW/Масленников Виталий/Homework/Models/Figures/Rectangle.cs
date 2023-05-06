using Newtonsoft.Json;

namespace Homework.Models.Figures
{
	public sealed class Rectangle : IFigure
	{
		private double _a;
		private double _b;
		
		public string Name => "Прямоугольник";
		public string Image => "rectangle.png";
		
		public int Id { get; set; }
		
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

		[JsonIgnore]
		public Dictionary<string, double> Parameters => new()
		{
			["a"] = _a,
			["b"] = _b,
		};

		public Rectangle(int id, double sideA, double sideB)
		{
			Id = id;
			(SideA, SideB) = (sideA, sideB);
		}

		[JsonIgnore] public double Area => SideB * SideA;
		[JsonIgnore] public double Perimeter => 2d * (SideA + SideA);
	}
}