using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class SwPersons : ViewComponent
{
    private readonly SWAPIService _service;
    
    public SwPersons(SWAPIService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync() => View(await _service.GetPersonsAsync());
}