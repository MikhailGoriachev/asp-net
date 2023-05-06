using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class PlanetInfo : ViewComponent
{
    private readonly SWAPIService _service;
    
    public PlanetInfo(SWAPIService service) => _service = service;
    
    public async Task<IViewComponentResult> InvokeAsync(int id) => 
        View(await _service.GetPlanetInfoAsync(id));
}