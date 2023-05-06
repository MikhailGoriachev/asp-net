
namespace Homework.Models.Figures
{
	public interface IFigure
	{
		int Id { get; set; }
		string Name { get; }

		string Image { get;}

		double Area { get; }

		double Perimeter { get; }

		public Dictionary<string, double> Parameters { get; }
	}
}