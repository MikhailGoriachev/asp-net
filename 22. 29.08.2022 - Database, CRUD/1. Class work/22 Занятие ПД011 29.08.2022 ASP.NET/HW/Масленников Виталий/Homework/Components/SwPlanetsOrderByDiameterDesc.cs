using System.Globalization;
using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwPlanetsOrderByDiameterDesc: ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwPlanetsOrderByDiameterDesc(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(int amount)
    {
        var planets = (await _service.GetPlanetsListAsync(amount))
            .OrderByDescending(p =>
            {
                var parse = int.TryParse(p.Diameter, out var mass);
                return parse ? mass : int.MaxValue;
            }).ToList();

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