using DITypeFilterSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DITypeFilterSample.Infrastructure;

public class ExceptionLogingAttribute : ExceptionFilterAttribute
{
    // ссылка для внедрения зависимости 
    private readonly Logger _logger;

    // !!! конструктор для внедрения зависимости !!!
    public ExceptionLogingAttribute(Logger logger) {
        this._logger = logger;
    }

    // реализация фильтра 
    public override void OnException(ExceptionContext context) {
        // в соответствии с SOLID: SRP -> Single Reponsibility Principal
        // обращение к сервису логирования 
        _logger.WriteLog($"Message: " + context.Exception.Message);
        _logger.WriteLog("Stack Trace: " + context.Exception.StackTrace);

        // возврат другого представления в случае ошибки
        context.Result = new ViewResult() { ViewName = "Error" };
    }
} // class ExceptionLogingAttribute
