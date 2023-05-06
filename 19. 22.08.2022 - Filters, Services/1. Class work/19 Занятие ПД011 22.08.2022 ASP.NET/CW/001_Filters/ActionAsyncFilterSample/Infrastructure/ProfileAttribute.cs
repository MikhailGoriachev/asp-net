using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionAsyncFilterSample.Infrastructure;

public class ProfileAttribute : ActionFilterAttribute
{
    private Stopwatch _timer = null!;

    // асинхронная реализация фильтра
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        _timer = Stopwatch.StartNew();

        // до вызова делегата ActionExecutionDelegate, код который запустится
        // перед методом действия
        await next();    // вызов метода действия
        // после вызова делегата ActionExecutionDelegate, код который запустится
        // по завершению метода действия

        _timer.Stop();
        string message = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - На выполнение метода " +
                         $"{context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed:g}";

        // StreamWriter streamWriter = new StreamWriter("App_Data/log.txt", true);
        // await streamWriter.WriteLineAsync(message);
        // streamWriter.Close();

        await File.AppendAllTextAsync("App_Data/log.txt", $"{message}\n", System.Text.Encoding.UTF8);
    }

} // class ProfileAttribute