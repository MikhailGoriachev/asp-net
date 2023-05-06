namespace Homework.Models.ViewModels;


public class PaginationLinksViewModel
{
    public int LinksCount { get; set; }
    public int CurrentPage { get; init; }
    public int PagesCount { get; init; }

    public List<RouteParameter>? AdditionalRouteParameters { get; set; }
}