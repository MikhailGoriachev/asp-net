using Microsoft.AspNetCore.Mvc.Filters;
using Home_work.Services;

namespace Home_work.Infrastructure;

public class ActionClientAttribute:ActionFilterAttribute
{
    private Logger _logger;
    
    //Конструктор с внедрением зависимостей
    public ActionClientAttribute(Logger logger)
    {
        _logger = logger;
    }

    //Завершение работы метода действия - выполнение опреации
    public override void OnActionExecuted(ActionExecutedContext context)
    {

        bool AddingMode = context.HttpContext.Request.Query["addingmode"][0].ToLower().Contains("true");

        var Id =        context?.ModelState["Id"].AttemptedValue;
        var Name =        context?.ModelState["Name"].AttemptedValue;
        var Surname =     context?.ModelState["Surname"].AttemptedValue;
        var Patronymic =  context?.ModelState["Patronymic"].AttemptedValue;
        var PhoneNumber = context?.ModelState["PhoneNumber"].AttemptedValue;
        var Email =       context?.ModelState["Email"].AttemptedValue;
        bool IsConstant =  context.ModelState["IsConstant"].AttemptedValue.ToLower().Contains("true");
        var Password =    context?.ModelState["Password"].AttemptedValue;
        var Age =         context?.ModelState["Age"].AttemptedValue;

        //При добавлении - зашифровать пароль
        if(AddingMode)
            Password = Utils.EncodeString(Password);

        string logStr = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {(AddingMode ? "Добавление": "Редактирование")} клиента\n" +
            $"\tId: {Id}\n" +
            $"\tФИО: {Surname} {Name} {Patronymic}\n" +
            $"\tНомер телефона: {PhoneNumber}\n" +
            $"\tE-mail: {Email}\n" +
            $"\tВозраст: {Age}\n" +
            $"\tПароль: {Password}\n" +
            $"\tПризнак постоянного клиента: {(IsConstant?"Постоянный":"Не постоянный")}";

        _logger.WrietInLog(logStr);
    }

}
