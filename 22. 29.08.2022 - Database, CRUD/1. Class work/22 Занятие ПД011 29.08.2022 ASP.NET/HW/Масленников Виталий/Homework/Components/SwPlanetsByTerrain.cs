using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwPlanetsByTerrain : ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwPlanetsByTerrain(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(string terrain, int amount)
    {
        var planets = (await _service.GetPlanetsListAsync(amount))
            .Where(p => p.Terrain.Contains(terrain)).ToList();

        var diameters = planets.GetDiameters();
        
        ViewBag.AvgDiameter = diameters.Average();
        ViewBag.MinDiameter = diameters.Min();
        ViewBag.MaxDiameter = diameters.Max();

        var orbitals = planets.GetOrbitals();

        ViewBag.AvgOrbital = orbitals.Average();
        ViewBag.MinOrbital = orbitals.Min();
        ViewBag.MaxOrbital = orbitals.Max();
        
        return View("~/Views/Planets/Components/SwPlanets/Default.cshtml" , planets);
    }
}