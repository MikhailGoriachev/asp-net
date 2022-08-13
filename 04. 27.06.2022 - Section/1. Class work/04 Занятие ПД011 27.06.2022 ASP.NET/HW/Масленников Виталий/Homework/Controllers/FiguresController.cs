using Homework.Infrastructure;
using Homework.Models.Figures;
using Newtonsoft.Json;

namespace Homework.Controllers;

// Контроллер для коллекции фигур
public class FiguresController
{
	private static readonly string DataFile = $"{Environment.CurrentDirectory}\\App_Data\\figures.json";
	
	private readonly double _sideMin;
	private readonly double _sideMax;

	// коллекция фигур
	public List<IFigure>? Figures { get; private set; }

	// ссылки на методы генерации
	private readonly List<Func<double, double, IFigure>> _generates = new ()
	{
		Square.Generate, Rectangle.Generate, Triangle.Generate
	};

	// конструктор
	public FiguresController(int amount = 12, double sideMin = 1, double sideMax = 10)
	{
		_sideMin = sideMin;
		_sideMax = sideMax;

		//if (File.Exists(DataFile))
		//	Deserialize();
		//else
			Generate(amount);
	}

	// генерация коллекции
	public void Generate(int amount = 12)
	{
		Figures = Enumerable.Repeat(0, amount).Select(_ => _generates.Random()!(_sideMin, _sideMax)).ToList();
		//Serialize();
	}

	// добавить фигуру в начало коллекции
	public void Add(IFigure? figure = null)
	{
		Figures!.Insert(0, figure ?? _generates.Random()!(_sideMin, _sideMax));
		//Serialize();
	}

	// получить упорядоченные по параметрам данные
	public List<IFigure> GetOrdered(string prop, string order) => FiguresController.GetOrdered(Figures, prop, order);
	public static List<IFigure> GetOrdered(List<IFigure>? figures, string prop, string order) =>
		order == "Asc"
			? figures!.AsQueryable().OrderBy(prop).ToList()
			: figures!.AsQueryable().OrderByDescending(prop).ToList();

	// сериализация в файл
	private void Serialize() =>
		File.WriteAllText(DataFile, JsonConvert.SerializeObject(Figures, Formatting.Indented,
			new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects,
			}));

	// десериализация из файла
	private void Deserialize() =>
		Figures = JsonConvert.DeserializeObject<List<IFigure>>(File.ReadAllText(DataFile),
			new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects,
			})!;
}