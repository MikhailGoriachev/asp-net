using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwPlanets : ViewComponent
{
    private readonly SWAPIService _swapiService;
    
    public SwPlanets(SWAPIService service) => _swapiService = service;
    
    public async Task<IViewComponentResult> InvokeAsync(int amount)
    {
        var planets = await _swapiService.GetPlanetsListAsync(amount);

        var diameters = planets.GetDiameters();
        
        ViewBag.AvgDiameter = diameters.Average();
        ViewBag.MinDiameter = diameters.Min();
        ViewBag.MaxDiameter = diameters.Max();

        var orbitals = planets.GetOrbitals();

        ViewBag.AvgOrbital = orbitals.Average();
        ViewBag.MinOrbital = orbitals.Min();
        ViewBag.MaxOrbital = orbitals.Max();

        return View(planets);
    }
}