using System.Globalization;
using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwCharactersOrderByMass : ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwCharactersOrderByMass(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(int amount)
    {
        var characters = (await _service.GetCharactersListAsync(amount))
            .OrderBy(c => {
                var parse = double.TryParse(c.Mass.Replace(",",""),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out double mass);
                return parse ? mass : int.MaxValue;
            }).ToList();

        var masses = characters.GetMasses();
        ViewBag.AvgMass = masses.Average();
        ViewBag.MinMaxMassDiff = masses.Max() - masses.Min();
        
        return View("~/Views/Characters/Components/SwCharacters/Default.cshtml" ,
            characters);
    }
}