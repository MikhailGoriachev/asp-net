using System.Globalization;
using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwPlanetsByDiameter: ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwPlanetsByDiameter(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(int from, int to, int amount)
    {
        var planets = await _service.GetPlanetsListAsync(amount);

        planets = planets.Where(p => {
            var parse = int.TryParse(p.Diameter, out int diameter);
            return parse && diameter >= from && diameter <= to;
        }).ToList();
        
        var diameters = planets.GetDiameters();
        
        ViewBag.AvgDiameter = diameters.Average();
        ViewBag.MinDiameter = diameters.Min();
        ViewBag.MaxDiameter = diameters.Max();

        var orbitals = planets.GetOrbitals();

        ViewBag.AvgOrbital = orbitals.Average();
        ViewBag.MinOrbital = orbitals.Min();
        ViewBag.MaxOrbital = orbitals.Max();

        return View("~/Views/Planets/Components/SwPlanets/Default.cshtml" ,planets);
    }
}