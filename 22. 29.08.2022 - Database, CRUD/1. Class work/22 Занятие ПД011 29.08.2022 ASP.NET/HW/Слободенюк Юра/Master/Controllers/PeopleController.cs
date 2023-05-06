using Master.Common;
using Master.Services;
using Master.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Master.Controllers;


public sealed class PeopleController : Controller
{
    private readonly ICharactersService _charactersService;


    public PeopleController(ICharactersService charactersService) =>
        _charactersService = charactersService;


    [HttpGet(Routes.People.Index)]
    public async Task<IActionResult> Index([FromQuery] int page = 1)
    {
        var paginated = await _charactersService.GetCharactersAsync(page);

        return View(new PeopleIndexViewModel
        {
            CurrentPage = page,
            PagesCount = (int)Math.Ceiling(paginated.Total / (double)10),
            Characters = paginated.Results!
        });
    }


    [HttpGet(Routes.People.Details)]
    public async Task<IActionResult> Details(int id)
    {
        var character = await _charactersService.GetCharacterAsync(id);

        return View(new PeopleDetailsViewModel
        {
            Name = character.Name,
            Height = character.Height,
            Mass = character.Mass,
            HairColor = character.HairColor,
            SkinColor = character.SkinColor,
            EyeColor = character.EyeColor,
            BirthYear = character.BirthYear,
            Gender = character.Gender,
            Homeworld = character.Homeworld,
        });
    }


    [HttpGet(Routes.People.ByMass)]
    public async Task<IActionResult> ByMass([FromQuery] int from, [FromQuery] int to, [FromQuery] int page = 1)
    {
        var paginated = await _charactersService.GetCharactersAsync(page);

        return View("Index",
            new PeopleIndexViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling(paginated.Total / (double)10),
                Characters = paginated.Results!
                    .Where(x =>
                    {
                        var isMass = int.TryParse(x.Mass, out var mass);

                        return isMass && (mass >= from && mass <= to);
                    })
                    .ToArray()
            });
    }


    [HttpGet(Routes.People.ByHomeworld)]
    public async Task<IActionResult> ByHomeworld([FromQuery] string homeworld, [FromQuery] int page = 1)
    {
        var paginated = await _charactersService.GetCharactersAsync(page);

        return View("Index",
            new PeopleIndexViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling(paginated.Total / (double)10),
                Characters = paginated.Results!
                    .Where(x => x.Homeworld!.Contains(homeworld, StringComparison.OrdinalIgnoreCase))
                    .ToArray()
            });
    }


    [HttpGet(Routes.People.MassAscending)]
    public async Task<IActionResult> MassAscending([FromQuery] int page = 1)
    {
        var paginated = await _charactersService.GetCharactersAsync(page);

        return View("Index",
            new PeopleIndexViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling(paginated.Total / (double)10),
                Characters = paginated.Results!.OrderBy(x =>
                {
                    var isMass = int.TryParse(x.Mass, out var mass);

                    return isMass ? mass : int.MaxValue;
                }).ToArray()
            });
    }
}