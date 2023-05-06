using ASP_NET_15_FormAspNet.Models;
using ASP_NET_15_FormAspNet.Models.Figures;

namespace ASP_NET_15_FormAspNet.Infrastructure;
  
public static class Utilities
{
	public static Random Rand = new Random();
 
	public static double GetDouble(double from, double to) => from + Rand.NextDouble() * (to - from);
		
	public static int GetInt(int from, int to) => Rand.Next(from, to);
 
	//-----------------------------------------------------------
	
	// Случайная генерация книги
	public static Book GetBook(int id) {
		(string fio, string name)[] fioName = new[] {
			("Пушкин А. С.", "Произведение Пушкина"), ("Чехов А. П.", "Произведение Чехова"),
			("Островский А. Н.", "Произведение Островского"),
			("Карамзин Н. М.", "Произведение Карамзина"), ("Катаев В. П.", "Произведение Катаева"),
			("Радищев А. Н.", "Произведение Радищев"),
			("Солженицын Александр Исаевич", "Произведение Солженицына"),
			("Сологуб Федор Кузьмич", "Произведение Сологуба"),
			("Толстой Александр Константинович", "Произведение Толстого"),
			("Успенский Глеб Иванович", "Произведение Успенского")
		};
		
		int idFioName = GetInt(0, 10);
		string fio = fioName[idFioName].fio;
		string name = fioName[idFioName].name;
		int year = GetInt(1700, 1950);
		int price= GetInt(120, 1250);
		int amount= GetInt(5, 555);
		int idCover = GetInt(1, 9);
		string cover = "templateBook" + $"{idCover}" + ".jpg";
		return new Book(id, fio, name, year, price, amount, cover); 
	}
	
	//-----------------------------------------------------------
	// идентификатор для фигуры
	public static int Identifier { get; private set; } = 0;
	
	// получить фигуру
	public static IFigure GetFigure( int id, int? type = null) {
		type ??= GetInt(0, 3);
  
		return type switch {
			0 => new Square { Id = id, Side = GetDouble(12d, 20d) },
			1 => new Triangle { Id = id, Sides = (GetDouble(10d, 12d), GetDouble(10d, 12d), GetDouble(10d, 12d)) },
			_ => new Rectangle {Id = id, SideA = GetDouble(10d, 12d), SideB = GetDouble(10d, 12d) }
		};
	}


	// получить коллекцию фигур
	public static List<IFigure> GetFigurList(int count) =>
		Enumerable.Repeat(0, count)
		.Select(i => GetFigure(Identifier++))
		.ToList();
	
}