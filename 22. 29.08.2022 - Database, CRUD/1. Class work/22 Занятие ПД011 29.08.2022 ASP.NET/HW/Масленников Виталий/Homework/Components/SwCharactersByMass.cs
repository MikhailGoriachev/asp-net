using System.Globalization;
using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwCharactersByMass : ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwCharactersByMass(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(int from, int to, int amount)
    {
        var characters = (await _service.GetCharactersListAsync(amount))
            .Where(c => {
                var parse = int.TryParse(c.Mass, out int mass);
                return parse && mass >= from && mass <= to;
            }).ToList();

        var masses = characters.GetMasses();
        ViewBag.AvgMass = masses.Average();
        ViewBag.MinMaxMassDiff = masses.Max() - masses.Min(); 
        
        return View("~/Views/Characters/Components/SwCharacters/Default.cshtml" ,characters);
    }
}