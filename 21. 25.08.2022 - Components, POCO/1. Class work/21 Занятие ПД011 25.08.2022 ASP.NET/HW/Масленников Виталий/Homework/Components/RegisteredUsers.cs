using Homework.Models;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class RegisteredUsers : ViewComponent
{
    private readonly UserService _service;
    
    public RegisteredUsers(UserService service) => _service = service;

    public async Task<IViewComponentResult> InvokeAsync() => View(await _service.GetUsersAsync());
} 