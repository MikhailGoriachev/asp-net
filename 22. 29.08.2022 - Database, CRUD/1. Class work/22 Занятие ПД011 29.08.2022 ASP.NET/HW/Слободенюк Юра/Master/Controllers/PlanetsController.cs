using Master.Common;
using Master.Services;
using Master.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Master.Controllers;


public class PlanetsController : Controller
{
    private readonly IPlanetsService _planetsService;


    public PlanetsController(IPlanetsService planetsService) =>
        _planetsService = planetsService;


    [HttpGet(Routes.Planets.Index)]
    public async Task<IActionResult> Index([FromQuery] int page = 1)
    {
        var paginated = await _planetsService.GetPlanetsAsync(page);

        return View(new PlanetsIndexViewModel
        {
            CurrentPage = page,
            PagesCount = (int)Math.Ceiling(paginated.Total / (double)10),
            Planets = paginated.Results!
        });
    }


    [HttpGet(Routes.Planets.Details)]
    public async Task<IActionResult> Details(int id)
    {
        var planet = await _planetsService.GetPlanetAsync(id);

        return View(new PlanetsDetailsViewModel
        {
            Name = planet.Name,
            RotationPeriod = planet.RotationPeriod,
            OrbitalPeriod = planet.OrbitalPeriod,
            Climate = planet.Climate,
            Gravity = planet.Gravity,
            Terrain = planet.Terrain,
            SurfaceWater = planet.SurfaceWater,
            Population = planet.Population,
            Diameter = planet.Diameter,
        });
    }


    [HttpGet(Routes.Planets.ByDiameter)]
    public async Task<IActionResult> ByDiameter([FromQuery] int from, [FromQuery] int to, [FromQuery] int page = 1)
    {
        var paginated = await _planetsService.GetPlanetsAsync(page);

        return View("Index",
            new PlanetsIndexViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling(paginated.Total / (double)10),
                Planets = paginated.Results!
                    .Where(x =>
                    {
                        var isDiameter = int.TryParse(x.Diameter, out var diameter);

                        return isDiameter && (diameter >= from && diameter <= to);
                    })
                    .ToArray()
            });
    }
    
    
    [HttpGet(Routes.Planets.ByTerrain)]
    public async Task<IActionResult> ByTerrain([FromQuery] ICollection<string> terrains, [FromQuery] int page = 1)
    {
        var paginated = await _planetsService.GetPlanetsAsync(page);

        return View("Index",
            new PlanetsIndexViewModel
            {
                CurrentPage = page,
                PagesCount = (int)Math.Ceiling(paginated.Total / (double)10),
                Planets = paginated.Results!
                    .Where(x => x.GetTerrains().Intersect(terrains).Any())
                    .ToArray()
            });
    }
}