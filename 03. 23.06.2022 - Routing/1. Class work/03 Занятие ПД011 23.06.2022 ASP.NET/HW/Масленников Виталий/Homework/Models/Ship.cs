namespace Homework.Models
{
	/*
	 Парусный корабль

	-длина в метрах,
	-ширина в метрах,
	-водоизмещение в тоннах,
	-название,
	-год постройки,
	-изображение	 
	 */

	public class Ship
	{
		public string? Name { get; set; }
		public string? Type { get; set; }
		public string? Image { get; set; }
		public float Length { get; set; }
		public double Width { get; set; }
		public double Displacement { get; set; }
		public int Year { get; set; }

	}
}
