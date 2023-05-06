using Newtonsoft.Json;

namespace Homework.Models.Figures
{

// Контейнер для коллекции фигур
	public class FiguresRepository : IFiguresRepository
	{
		private static readonly string DataFile = $"{Environment.CurrentDirectory}\\App_Data\\figures.json";

		private readonly double _sideMin;
		private readonly double _sideMax;

		private List<IFigure> _figures = null!;
	
		public IEnumerable<IFigure> Figures => _figures;
		
		public FiguresRepository(int amount = 12, double sideMin = 1, double sideMax = 10)
		{
			_sideMin = sideMin;
			_sideMax = sideMax;

			if (File.Exists(DataFile))
				Deserialize();
			else
			{
				Generate(amount);
				Serialize();
			}
		}
		
		public void Add(IFigure? figure = null) => 
			_figures.Insert(0, figure ?? new FiguresFactory(_sideMin, _sideMax)
				.RandomFigure(_figures.Any() ? _figures.Max(f => f.Id) + 1 : 1));
		
		public IFigure Get(int id) => _figures.FirstOrDefault(f => f.Id == id)!;
		
		public void Update(IFigure figure)
		{
			var find = _figures.FindIndex(f => f.Id == figure.Id);
			if (find == -1) return;
			_figures[find] = figure;
		}

		public void Delete(int id)
		{
			var delete = _figures.FirstOrDefault(f => f.Id == id);
        
			if(delete is not null)
				_figures.Remove(delete);
		}
		
		public Dictionary<string, int> FiguresCounts() => new ()
		{
			["Квадрат"] = AmountOf("Квадрат"),
			["Прямоугольник"] = AmountOf("Прямоугольник"),
			["Треугольник"] = AmountOf("Треугольник"),
		};

		public void SaveData() => Serialize();
		
		private int AmountOf(string type) => _figures.Count(f => f.Name == type);
		
		private void Generate(int amount = 12) =>
			_figures = new FiguresFactory(_sideMin, _sideMax).GenerateCollection(amount);
		
		private void Serialize() =>
			File.WriteAllText(DataFile, JsonConvert.SerializeObject(Figures, Formatting.Indented,
				new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Objects,
				}));

		private void Deserialize() =>
			_figures = JsonConvert.DeserializeObject<List<IFigure>>(File.ReadAllText(DataFile),
				new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Objects,
				})!;
	}
}