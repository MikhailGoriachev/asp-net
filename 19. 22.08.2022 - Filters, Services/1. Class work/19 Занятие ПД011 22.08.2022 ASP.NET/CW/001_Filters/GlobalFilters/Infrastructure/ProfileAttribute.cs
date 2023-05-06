using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalFilters.Infrastructure;

// Фильтр действия
public class ProfileAttribute : ActionFilterAttribute
{
    private Stopwatch _timer = null!;

    public override void OnActionExecuting(ActionExecutingContext context) {
        _timer = Stopwatch.StartNew();
    }

    public override void OnActionExecuted(ActionExecutedContext context) {
        _timer.Stop();
        string message = $"На выполнение метода {context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed}";

        StreamWriter streamWriter = new StreamWriter("App_Data/log.txt", true);
        streamWriter.WriteLine(message);
        streamWriter.Close();
    }
}
