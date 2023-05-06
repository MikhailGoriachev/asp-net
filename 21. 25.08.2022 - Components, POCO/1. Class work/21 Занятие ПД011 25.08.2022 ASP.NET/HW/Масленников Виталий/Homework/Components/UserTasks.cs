using Homework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.Components;

public class UserTasks : ViewComponent
{
    private readonly UserService _service;
    
    public UserTasks(UserService service) => _service = service;
    
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var tasks = await _service.GetUserTasksAsync(id);

        ViewBag.TasksCount = tasks.Count;
        ViewBag.TasksCompleted = tasks.Count(t => t.Completed);
        ViewBag.TasksIncompleted = tasks.Count - ViewBag.TasksCompleted;

        return View(tasks);
    }
    
}