using Microsoft.AspNetCore.Mvc.Filters;
using Home_work.Services;
using System.Diagnostics;

namespace Home_work.Infrastructure;

public class HybridFilterInstructorsAttribute : ActionFilterAttribute
{
    private Logger _logger;
    private Stopwatch _timer;

    //Конструктор с внедрением зависимостей
    public HybridFilterInstructorsAttribute(Logger logger)
    {
        _logger = logger;
    }

    //Перед исполнением метода действия
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _timer = Stopwatch.StartNew();
    }


    //После исполнением метода действия
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _timer.Stop();

        string logMessage  = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - На выполнение метода действия " +
                            $"{context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed:g}";

        //Запись в log файл
        _logger.WrietInLog(logMessage);

    }


    //Перед началом формированием ответа
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        _timer = Stopwatch.StartNew();
    }

    //После формирования результата
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        _timer.Stop();

        string logMessage = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - На формирование результата " +
                            $"{context.ActionDescriptor.DisplayName} затрачено {_timer.Elapsed:g}";

        //Запись в log файл
        _logger.WrietInLog(logMessage);
    }

}
