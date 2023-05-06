using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class CharacterInfo : ViewComponent
{
    private readonly SWAPIService _service;
    
    public CharacterInfo(SWAPIService service) => _service = service;
    
    public async Task<IViewComponentResult> InvokeAsync(int id) => 
        View(await _service.GetCharacterInfoAsync(id));
}