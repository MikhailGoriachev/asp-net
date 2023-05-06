using Master.Models;


namespace Master.ViewModels;


public sealed class PeopleIndexViewModel
{
    public int CurrentPage { get; init; }
    public int PagesCount { get; init; }
    public IReadOnlyCollection<Character> Characters { get; init; } = null!;
}