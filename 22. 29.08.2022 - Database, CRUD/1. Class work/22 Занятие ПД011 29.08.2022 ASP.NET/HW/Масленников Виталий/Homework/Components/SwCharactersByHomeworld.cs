using System.Globalization;
using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwCharactersByHomeworld : ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwCharactersByHomeworld(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(string homeworld, int amount)
    {
        var characters = (await _service.GetCharactersListAsync(amount))
            .Where(c => c.Homeworld == homeworld).ToList();

        var masses = characters.GetMasses();
        ViewBag.AvgMass = masses.Average();
        ViewBag.MinMaxMassDiff = masses.Max() - masses.Min(); 
        
        return View("~/Views/Characters/Components/SwCharacters/Default.cshtml" ,characters);
    }
}