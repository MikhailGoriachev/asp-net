using Microsoft.AspNetCore.Mvc.Filters;
using Home_work.Services;
using System.Diagnostics;

namespace Home_work.Infrastructure;

public class ActionsFilterAttribute : ActionFilterAttribute
{
    private Logger _logger;
    private Stopwatch _timer;

    //Конструктор с внедрением зависимостей
    public ActionsFilterAttribute(Logger logger)
    {
        _logger = logger;
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

        string logMessage = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] - Формирование результата: " +
                            $"{context.ActionDescriptor.DisplayName}\n Затрачено времени: {_timer.Elapsed:g}";

        //Запись в log файл
        _logger.WrietInLog(logMessage);
    }

}
