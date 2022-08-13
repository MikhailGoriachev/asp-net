namespace Homework.Models
{
	/* Модель описания города
	 * название, год основания, герб, население на текущий момент, площадь.
	 */

	public class City
	{
		public string? Name { get; set; }
		
		public int FoundationYear { get; set; }
		
		public int Population { get; set; }
		
		public double Area { get; set; }

		public string? CoatOfArms { get; set; }
	}
}
