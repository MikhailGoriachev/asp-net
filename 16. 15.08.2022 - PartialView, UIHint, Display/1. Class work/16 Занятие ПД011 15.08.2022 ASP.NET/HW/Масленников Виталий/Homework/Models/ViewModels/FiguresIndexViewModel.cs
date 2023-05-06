using Homework.Models.Figures;

namespace Homework.Models.ViewModels;

public class FiguresIndexViewModel
{
    public IEnumerable<IFigure> Figures { get; set; } = null!;

    public Dictionary<string, int> FiguresCounts = null!;
}