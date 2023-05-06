using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class UserPosts : ViewComponent
{
    private readonly UserService _service;
    
    public UserPosts(UserService service) => _service = service;
    
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var posts = await _service.GetUserPostsAsync(id);

        ViewBag.PostsCount = posts.Count();
        ViewBag.MinLength = posts.Min(p => p.Body.Length);
        ViewBag.MaxLength = posts.Max(p => p.Body.Length);
        ViewBag.AvgLength = posts.Average(p => p.Body.Length);
        
        return View(posts);
    }
}