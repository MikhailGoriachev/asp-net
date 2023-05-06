using HomeWork.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HomeWork.Filters;

public class ExceptionLoggingAttribute : ExceptionFilterAttribute
{
    public Logger Logger { get; set; }

    #region Конструкторы

    // конструктор инициализирующий
    public ExceptionLoggingAttribute(Logger logger)
    {
        Logger = logger;
    }

    #endregion

    #region Методы

    // обработка исключения
    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        await Logger.WriteLogAsync(
            $"Message = {context.Exception.Message}; Action = {context.ActionDescriptor.DisplayName};\r\n{context.Exception.StackTrace};\r\n");

        context.Result = new ViewResult { ViewName = "Error" };
    }

    #endregion
}
