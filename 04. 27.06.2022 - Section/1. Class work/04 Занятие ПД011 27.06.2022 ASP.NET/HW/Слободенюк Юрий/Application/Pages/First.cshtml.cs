using Application.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Application.Pages;


public sealed class First : PageModel
{
    private readonly string _contentRootPath;


    public First(IWebHostEnvironment env)
    {
        _contentRootPath = env.WebRootPath;
    }


    [BindProperty]
    public Product Input { get; set; } = null!;


    public IEnumerable<string> GetPossibleImageNames()
    {
        var imagesDirectory = Path.Combine(_contentRootPath, "images");
        return Directory.EnumerateFiles(imagesDirectory)
            .Select(Path.GetFileName)
            .ToList()!;
    }
}