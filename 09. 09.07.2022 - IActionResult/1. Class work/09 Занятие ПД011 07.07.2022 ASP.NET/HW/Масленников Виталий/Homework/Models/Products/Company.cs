using System;
using System.Collections.Generic;
using System.IO;
using Homework.Models.Figures;
using Newtonsoft.Json;
using Homework.Infrastructure;

namespace Homework.Models.Products
{
	public class Company
	{
		private static readonly string DataFile = $"{Environment.CurrentDirectory}\\App_Data\\products.json";


		public List<Product>? Products { get; private set; }

		public Company()
		{
			if (File.Exists(DataFile))
				Deserialize();
			else
			{
				Products = new List<Product>
				{
					new("Аэрогриль", "ART001", "Аэрогриль", 3200, 2, "air_fryer.jpeg"),
					new("Шашлычница", "ART002", "Шашлычница", 6100, 5, "brazier.jpeg"),
					new("Хлебопечка", "ART003", "Хлебопечка", 3400, 4, "breadmaker.jpeg"),
					new("Пылесос", "ART004", "Пылесос", 6100, 6, "cleaner.jpeg"),
					new("Кухонная вытяжка", "ART005", "Вытяжка", 2200, 2, "cooker_hood.jpeg"),
					new("Фритюрница", "ART006", "Фритюрница", 1540, 1, "fryer.jpeg"),
					new("Утюг", "ART007", "Утюг", 700, 8, "iron.jpeg"),
					new("Микроволновая печь", "ART008", "Печь", 11100, 4, "microwave_oven.jpeg"),
					new("Мини печь", "ART009", "Печь", 6000, 2, "mini_oven.jpeg"),
					new("Мультиварка", "ART010", "Мультиварка", 7700, 5, "multicooker.jpeg"),
					new("Плита кухонная", "ART011", "Плита", 16400, 1, "plate.jpeg"),
					new("Холодильник", "ART012", "Холодильник", 23800, 6, "refrigerator.jpeg"),
					new("Пароварка", "ART013", "Пароварка", 5500, 2, "steamer.jpeg"),
					new("Стиральная машина", "ART014", "Стиральная машина", 23100, 8, "washing_machine.jpeg"),
				};
				Serialize();
			}
		}

		public void Add(Product prod) => Products?.Insert(0, prod);

		public IEnumerable<Product> GetOrdered(string prop, string order) =>
			order == "Asc" ? Products!.OrderBy(prop) : Products!.OrderByDescending(prop);

		public void Serialize() =>
			File.WriteAllText(DataFile, JsonConvert.SerializeObject(Products, Formatting.Indented));

		public void Deserialize() =>
			Products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(DataFile));


		public static List<TypeInfo> Types = new()
		{
			new TypeInfo("Аэрогриль", "air_fryer.jpeg"),
			new TypeInfo("Шашлычница", "brazier.jpeg"),
			new TypeInfo("Хлебопечка", "breadmaker.jpeg"),
			new TypeInfo("Пылесос", "cleaner.jpeg"),
			new TypeInfo("Кухонная вытяжка", "cooker_hood.jpeg"),
			new TypeInfo("Фритюрница", "fryer.jpeg"),
			new TypeInfo("Утюг", "iron.jpeg"),
			new TypeInfo("Микроволновая печь", "microwave_oven.jpeg"),
			new TypeInfo("Мини печь", "mini_oven.jpeg"),
			new TypeInfo("Мультиварка", "multicooker.jpeg"),
			new TypeInfo("Плита кухонная", "plate.jpeg"),
			new TypeInfo("Холодильник", "refrigerator.jpeg"),
			new TypeInfo("Пароварка", "steamer.jpeg"),
			new TypeInfo("Стиральная машина", "washing_machine.jpeg"),
		};

		public record TypeInfo(string Type, string Image);
	}
}