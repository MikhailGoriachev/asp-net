using Homework.Common;

namespace Homework.Models.Figures
{
// Фабрика для геометрических фигур
	public class FiguresFactory
	{
		private readonly double _minSide;
		private readonly double _maxSide;

		// конструктор
		public FiguresFactory(double minSide, double maxSide)
		{
			_minSide = minSide;
			_maxSide = maxSide;
		}
		
		// коллекция случайных фигур
		public List<IFigure> GenerateCollection(int amount)
		{
			var id = 1;
			return Enumerable.Repeat(0, amount).Select(_ => RandomFigure(id++)).ToList();
		}

		// случайная фигура
		public IFigure RandomFigure(int id) =>
			new List<Func<int, double, double, IFigure>>
			{
				GenerateRectangle, GenerateSquare, GenerateTriangle
			}.Random()!(id, _minSide, _maxSide);

		// генерация прямоугольника
		public static IFigure GenerateRectangle(int id, double min, double max) =>
			new Rectangle(id, Random.Shared.NextDouble(min, max, 3),
				Random.Shared.NextDouble(min, max, 3));

		// генерация квадрата
		public static IFigure GenerateSquare(int id, double min, double max) =>
			new Square(id, Random.Shared.NextDouble(min, max, 3));

		// генерация треугольника
		public static IFigure GenerateTriangle(int id, double min, double max)
		{
			double a, b, c;
			do
			{
				a = Random.Shared.NextDouble(min, max, 3);
				b = Random.Shared.NextDouble(min, max, 3);
				c = Random.Shared.NextDouble(min, max, 3);
			} while (!Triangle.IsTriangle(a, b, c));

			return new Triangle(id, a, b, c);
		}
	}
}