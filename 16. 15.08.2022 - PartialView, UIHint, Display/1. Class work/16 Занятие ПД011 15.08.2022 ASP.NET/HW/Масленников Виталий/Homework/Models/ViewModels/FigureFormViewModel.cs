using Homework.Models.Figures;

namespace Homework.Models.ViewModels;

public class FigureFormViewModel
{
    public FigureInput FigureInput { get; set; } = null!;
    public string? ErrMsg { get; set; }
}