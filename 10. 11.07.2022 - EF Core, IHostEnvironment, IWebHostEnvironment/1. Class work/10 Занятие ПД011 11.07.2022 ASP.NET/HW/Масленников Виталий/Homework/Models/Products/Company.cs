using Newtonsoft.Json;
using Homework.Common;

namespace Homework.Models.Products
{
	public class Company
	{
		private const string DataFileName = "products.json";
		private readonly string _dataFolder;
		private string DataFullPath => Path.Combine(_dataFolder, DataFileName);
		
		public List<Product> Products { get; private set; } = new ();

		public Company(string dataFolder)
		{
			_dataFolder = dataFolder;
			
			if (File.Exists(DataFullPath))
				Deserialize();
			else
			{
				var seed = new List<Product>
				{
					new("Аэрогриль", "ART001", "Аэрогриль", 3200, 2),
					new("Шашлычница", "ART002", "Шашлычница", 6100, 5),
					new("Хлебопечка", "ART003", "Хлебопечка", 3400, 4),
					new("Пылесос", "ART004", "Пылесос", 6100, 6),
					new("Кухонная вытяжка", "ART005", "Вытяжка", 2200, 2),
					new("Фритюрница", "ART006", "Фритюрница", 1540, 1),
					new("Утюг", "ART007", "Утюг", 700, 8),
					new("Микроволновая печь", "ART008", "Микроволновая печь", 11100, 4),
					new("Мини печь", "ART009", "Мини печь", 6000, 2),
					new("Мультиварка", "ART010", "Мультиварка", 7700, 5),
					new("Плита кухонная", "ART011", "Плита", 16400, 1),
					new("Холодильник", "ART012", "Холодильник", 23800, 6),
					new("Пароварка", "ART013", "Пароварка", 5500, 2),
					new("Стиральная машина", "ART014", "Стиральная машина", 23100, 8),
				};
				
				seed.ForEach(Add);
				
				Serialize();
			}
		}

		public void Add(Product prod)
		{
			prod.Image ??= TypeImages[prod.Type!];
			Products.Insert(0, prod);
		}
		
		public IEnumerable<Product>? GetOrdered(string prop, string order) =>
			order == "Asc" ? Products?.OrderBy(prop) : Products.OrderByDescending(prop);

		public void Serialize() =>
			File.WriteAllText(DataFullPath, JsonConvert.SerializeObject(Products, Formatting.Indented));
		
		public void Deserialize() =>
			Products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(DataFullPath))!;

		public static readonly Dictionary<string, string> TypeImages = new()
		{
			["Аэрогриль"] = "air_fryer.jpeg",
			["Шашлычница"] = "brazier.jpeg",
			["Хлебопечка"] = "breadmaker.jpeg",
			["Пылесос"] = "cleaner.jpeg",
			["Вытяжка"] = "cooker_hood.jpeg",
			["Фритюрница"] = "fryer.jpeg",
			["Утюг"] = "iron.jpeg",
			["Микроволновая печь"] = "microwave_oven.jpeg",
			["Мини печь"] = "mini_oven.jpeg",
			["Мультиварка"] = "multicooker.jpeg",
			["Плита"] = "plate.jpeg",
			["Холодильник"] = "refrigerator.jpeg",
			["Пароварка"] = "steamer.jpeg",
			["Стиральная машина"] = "washing_machine.jpeg",
		};
	}
}