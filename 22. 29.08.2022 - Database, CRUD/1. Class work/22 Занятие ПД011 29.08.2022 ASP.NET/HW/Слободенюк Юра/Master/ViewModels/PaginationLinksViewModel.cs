using Master.Models;


namespace Master.ViewModels;


public sealed class PaginationLinksViewModel
{
    public int LinksCount { get; set; }
    public int CurrentPage { get; init; }
    public int PagesCount { get; init; }
}