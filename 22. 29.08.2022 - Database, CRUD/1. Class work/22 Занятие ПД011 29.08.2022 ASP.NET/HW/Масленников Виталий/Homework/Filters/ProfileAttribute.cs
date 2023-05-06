using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Homework.Common;

namespace Homework.Infrastructure;


public class ProfileAttribute : ActionFilterAttribute
{
    private Stopwatch _timer = new();
    private readonly Services.Logger _logger;

    public ProfileAttribute(Services.Logger logger) => _logger = logger;

    public override void OnActionExecuting(ActionExecutingContext context) => _timer = Stopwatch.StartNew();

    public override void OnActionExecuted(ActionExecutedContext context) {
        _timer.Stop();
        string message = $"[{new DateTime().NowDateTimeString()}] ACTION: " +
                         $"{context.ActionDescriptor.DisplayName} TIME ELAPSED: {_timer.Elapsed:g}";
        _logger.WriteLog(message);
    }

    public override void OnResultExecuting(ResultExecutingContext context) => _timer = Stopwatch.StartNew();

    public override void OnResultExecuted(ResultExecutedContext context) {
        _timer.Stop();
        string message = $"[{new DateTime().NowDateTimeString()}] RESULT OF: " +
                         $"{context.ActionDescriptor.DisplayName} TIME ELAPSED: {_timer.Elapsed:g}";
        _logger.WriteLog(message);
    }
}