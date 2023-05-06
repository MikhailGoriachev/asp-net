using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class UserPhotos : ViewComponent
{
    private readonly UserService _service;
    
    public UserPhotos(UserService service) => _service = service;
    
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var photos = await _service.GetUserPhotosAsync(id);

        ViewBag.Count = photos.Count;
        
        return View(photos);
    }
}