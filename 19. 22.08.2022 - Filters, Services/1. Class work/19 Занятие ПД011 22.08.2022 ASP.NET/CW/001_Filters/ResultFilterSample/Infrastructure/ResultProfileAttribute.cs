using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResultFilterSample.Infrastructure;

// Фильтр результата
public class ResultProfileAttribute : ResultFilterAttribute
{
    private Stopwatch _timer = null!;

    // метод запускается перед началом генерации ответа
    public override void OnResultExecuting(ResultExecutingContext context) {
        _timer = Stopwatch.StartNew();
    }

    // метод запускается после окончания генерации ответа - после
    // отработки кода в представлении
    public override void OnResultExecuted(ResultExecutedContext context) {
        _timer.Stop();
        string message = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - На формирование результата метода " +
                         $"{context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed:g}";

        File.AppendAllText("App_Data/log.txt", $"{message}\n", System.Text.Encoding.UTF8);
    } // OnResultExecuted

} // class ResultProfileAttribute
