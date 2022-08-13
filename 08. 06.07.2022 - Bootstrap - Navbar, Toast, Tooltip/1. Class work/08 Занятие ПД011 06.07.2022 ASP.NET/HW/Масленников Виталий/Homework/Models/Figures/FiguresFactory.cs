using Homework.Infrastructure;

namespace Homework.Models.Figures;

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

	// случайная фигура
	public IFigure RandomFigure() =>
		new List<Func<double, double, IFigure>> {
			GenerateRectangle, GenerateSquare, GenerateTriangle
		}.Random()!(_minSide, _maxSide);

	// коллекция случайных фигур
	public List<IFigure> GenerateCollection(int amount) =>
		Enumerable.Repeat(0, amount).Select(_ => RandomFigure()).ToList();

	// генерация прямоугольника
	public static IFigure GenerateRectangle(double min, double max) => 
		new Rectangle(Random.Shared.NextDouble(min, max), Random.Shared.NextDouble(min, max));

	// генерация квадрата
	public static IFigure GenerateSquare(double min, double max) => 
		new Square(Random.Shared.NextDouble(min, max));

	// генерация треугольника
	public static IFigure GenerateTriangle(double min, double max)
	{
		double a, b, c;
		do
		{
			a = Random.Shared.NextDouble(min, max);
			b = Random.Shared.NextDouble(min, max);
			c = Random.Shared.NextDouble(min, max);
		} while (!Triangle.IsTriangle(a, b, c));

		return new Triangle(a, b, c);
	}
}