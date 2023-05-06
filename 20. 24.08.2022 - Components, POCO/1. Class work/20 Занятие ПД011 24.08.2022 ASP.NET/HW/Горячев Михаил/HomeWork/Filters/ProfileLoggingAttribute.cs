using System.Diagnostics;
using HomeWork.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HomeWork.Filters;

public class ProfileLoggingAttribute : ActionFilterAttribute
{
    // логгер
    public Logger Logger { get; set; }

    // таймер
    public Stopwatch Timer { get; set; } = null!;

    #region Конструкторы

    public ProfileLoggingAttribute(Logger logger) =>
        Logger = logger;

    #endregion

    #region Методы

    // обработка работы result
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // запуск таймера
        Timer = Stopwatch.StartNew();

        // запись в логи сообщения о начале выполнения
        await Logger.WriteLogAsync(
            $"START Result: action - {context.ActionDescriptor.DisplayName}; time - {Timer.Elapsed:c}");

        // вызов метода действия
        await next();

        Timer.Stop();

        // запись в логи сообщения о завершении выполнения
        await Logger.WriteLogAsync(
            $"END   Result: action - {context.ActionDescriptor.DisplayName}; time - {Timer.Elapsed:c}");
    }


    // обработка работы result
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // запуск таймера
        Timer = Stopwatch.StartNew();

        // запись в логи сообщения о начале выполнения
        await Logger.WriteLogAsync(
            $"START Action: action - {context.ActionDescriptor.DisplayName}; time - {Timer.Elapsed:c}");

        // вызов метода действия
        await next();

        Timer.Stop();

        // запись в логи сообщения о завершении выполнения
        await Logger.WriteLogAsync(
            $"END   Action: action - {context.ActionDescriptor.DisplayName}; time - {Timer.Elapsed:c}");
    }

    #endregion
}
