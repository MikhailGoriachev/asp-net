using System.Globalization;
using Homework.Common;
using Homework.Models;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwCharacters : ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwCharacters(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync(int amount)
    {
        var characters = await _service.GetCharactersListAsync(amount);

        var masses = characters.GetMasses();
        ViewBag.AvgMass = masses.Average();
        ViewBag.MinMaxMassDiff = masses.Max() - masses.Min();

        return View(characters);
    }
    
}