using DITypeFilterSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DIServiceFilterSample.Infrastructure;

public class ExceptionLogingAttribute : ExceptionFilterAttribute
{
    private readonly Logger _logger;

    public ExceptionLogingAttribute(Logger logger) {
        this._logger = logger;
    }

    public override void OnException(ExceptionContext context) {
        _logger.WriteLog("Message: " + context.Exception.Message);
        _logger.WriteLog("Stack Trace: " + context.Exception.StackTrace);

        // возврат другого представления в случае ошибки
        context.Result = new ViewResult() { ViewName = "Error" };
    }
}
