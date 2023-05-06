using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExceptionFilterSample.Infrastructure;

// Фильтр исключений
public class ExceptionLogingAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context) {
        StreamWriter streamWriter = new StreamWriter("App_Data/log.txt", true);

        // context.Exception - исключение, из-за которого сработал фильтр
        streamWriter.WriteLine($"Message: {context.Exception.Message}\n" +
                               $"Stack Trace: {context.Exception.StackTrace}\n");

        streamWriter.Close();

        // возврат другого представления в случае ошибки
        context.Result = new ViewResult {
            ViewName = "Error"
        };
    } // OnException
} // class ExceptionLogingAttribute
