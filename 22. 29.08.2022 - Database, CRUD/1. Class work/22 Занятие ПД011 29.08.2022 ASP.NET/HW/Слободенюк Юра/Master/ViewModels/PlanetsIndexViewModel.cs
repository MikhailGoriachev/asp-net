using Master.Models;


namespace Master.ViewModels;


public sealed class PlanetsIndexViewModel
{
    public int CurrentPage { get; init; }
    public int PagesCount { get; init; }
    public IReadOnlyCollection<Planet> Planets { get; init; } = null!;
}