using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HybridFilterSample.Infrastructure;

// Гибридный фильтр - одновременно фильтр действия и фильтр результата
public class ProfileAttribute : ActionFilterAttribute
{
    private Stopwatch _timer = null!;

    public override void OnActionExecuting(ActionExecutingContext context) {
        _timer = Stopwatch.StartNew();
    }

    public override void OnActionExecuted(ActionExecutedContext context) {
        _timer.Stop();
        string message = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - На выполнение метода " +
                         $"{context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed:g}";

        File.AppendAllText("App_Data/log.txt", $"{message}\n", System.Text.Encoding.UTF8);
    }

    public override void OnResultExecuting(ResultExecutingContext context) {
        _timer = Stopwatch.StartNew();
    }

    public override void OnResultExecuted(ResultExecutedContext context) {
        _timer.Stop();
        string message = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - На формирование результата метода " +
                         $"{context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed:g}";

        File.AppendAllText("App_Data/log.txt", $"{message}\n", System.Text.Encoding.UTF8);
    }
}
