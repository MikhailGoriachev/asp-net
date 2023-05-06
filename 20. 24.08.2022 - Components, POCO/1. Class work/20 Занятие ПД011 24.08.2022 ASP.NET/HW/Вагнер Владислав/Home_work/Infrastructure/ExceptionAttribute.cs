using Microsoft.AspNetCore.Mvc.Filters;
using Home_work.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Home_work.Infrastructure;

public class ExceptionAttribute:ExceptionFilterAttribute
{
    private Logger _logger;

    //Конструктор с внедрением зависимостей
    public ExceptionAttribute(Logger logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {

        string logMessage = $"\n--{DateTime.Now:dd/MM/yyyy HH:mm} - Выброс исключения\n" +
                            $" Action method: {context.ActionDescriptor.DisplayName}\n" +
                            $" Message: {context.Exception.Message}\n" +
                            $" Stack trace: {context.Exception.StackTrace}";

        //Запись в log файл
        _logger.WrietInLog(logMessage);

        ViewResult viewResult = new ViewResult()
        {
            ViewName = "Exception",
            /*ViewData["Message"] = context.Exception.Message*/
        };

        context.Result = viewResult;

    }

}
