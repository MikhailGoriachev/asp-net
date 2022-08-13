namespace Homework.Models.Figures;

public interface IFigure
{
	string Name { get; }

	double Area { get; }

	double Perimeter { get; }

	string ToTableRow(int row, string path);
}