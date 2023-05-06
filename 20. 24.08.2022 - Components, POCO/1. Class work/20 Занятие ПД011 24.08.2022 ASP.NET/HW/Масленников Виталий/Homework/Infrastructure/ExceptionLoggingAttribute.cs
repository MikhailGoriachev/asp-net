using Homework.Common;
using Homework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Homework.Infrastructure;

public class ExceptionLoggingAttribute : ExceptionFilterAttribute
{
    private readonly Logger _logger;
    
    public ExceptionLoggingAttribute(Logger logger) => _logger = logger;

    public override void OnException(ExceptionContext context)
    {
        _logger.WriteLog($"[{new DateTime().NowDateTimeString()}] AN ERROR OCCURED");
        _logger.WriteLog("Message: " + context.Exception.Message);
        _logger.WriteLog("Stack Trace: " + context.Exception.StackTrace);
        
        context.Result = new ViewResult { ViewName = "Error"};
    }
}