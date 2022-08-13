using Homework.Infrastructure;
using Newtonsoft.Json;

namespace Homework.Models.Figures;

// Контейнер для коллекции фигур
public class FiguresCollection
{
	private static readonly string DataFile = $"{Environment.CurrentDirectory}\\App_Data\\figures.json";
	
	private readonly double _sideMin;
	private readonly double _sideMax;
	private int _amount;

	// коллекция фигур
	public List<IFigure>? Figures { get; private set; }


	// конструктор
	public FiguresCollection(int amount = 12, double sideMin = 1, double sideMax = 10)
	{
		_sideMin = sideMin;
		_sideMax = sideMax;
		_amount = amount;
		
		if (File.Exists(DataFile))
			Deserialize();
		else
		{
			Generate(_amount);
			Serialize();
		}
	}

	// генерация коллекции
	public void Generate(int amount = 12) => 
		Figures = new FiguresFactory(_sideMin, _sideMax).GenerateCollection(_amount);

	// добавить фигуру в начало коллекции
	public void Add(IFigure? figure = null) =>
		Figures!.Insert(0, figure ?? new FiguresFactory(_sideMin, _sideMax).RandomFigure());

	// получить упорядоченные по параметрам данные
	public List<IFigure> GetOrdered(string prop, string order) =>
		FiguresCollection.GetOrdered(Figures, prop, order);

	public static List<IFigure> GetOrdered(List<IFigure>? figures, string prop, string order) =>
		order == "Asc"
			? figures!.AsQueryable().OrderBy(prop).ToList()
			: figures!.AsQueryable().OrderByDescending(prop).ToList();


	// сериализация в файл
	public void Serialize() =>
		File.WriteAllText(DataFile, JsonConvert.SerializeObject(Figures, Formatting.Indented,
			new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects,
			}));

	// десериализация из файла
	public void Deserialize() =>
		Figures = JsonConvert.DeserializeObject<List<IFigure>>(File.ReadAllText(DataFile),
			new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects,
			})!;
}