using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.IO;

namespace ActionFilterSample.Infrastructure;

// Класс для построения атрибута Profile - для реализации фильтров действия
public class ProfileAttribute : ActionFilterAttribute
{
    // класс для подсчета времени
    private Stopwatch _timer = null!;

    // запуск перед действием
    public override void OnActionExecuting(ActionExecutingContext context) {
        _timer = Stopwatch.StartNew();
    } // OnActionExecuting


    // запуск по окончании действия
    public override void OnActionExecuted(ActionExecutedContext context) {
        _timer.Stop();

        string message = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - На выполнение метода " +
                         $"{context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed:g}";

        // при работе фильтра нет возможности вывода в пользовательский интерфейс
        using var streamWriter = new StreamWriter("App_Data/log.txt", true);
        streamWriter.WriteLine(message);
    } // OnActionExecuted
} // class ProfileAttribute
