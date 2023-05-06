using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Models.ViewModels;

public class IndexViewModel<T>
{
    public int CurrentPage { get; init; }
    
    public int PagesCount { get; init; }
    
    public List<T> Items { get; init; } = null!;

    public FilterViewModel? FilterModel { get; set; }
}