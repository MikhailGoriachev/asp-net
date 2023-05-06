using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Common;

public static class Extensions
{
    // Подстановка css-класса в зависимости от контроллера/действия
    public static string IsActive(this IHtmlHelper htmlHelper,
                                  string? controllers = null,
                                  string? actions = null,
                                  string cssClass = "active")
    {
        var currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;
        var currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;

        var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
        var acceptedActions = (actions ?? currentAction ?? "").Split(',');

        return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
            ? cssClass
            : "";
    }
    
    // Случайный элемент перечисляемой коллекции
    public static T? Random<T>(this IEnumerable<T> enumerable)
    {
        var list = enumerable as IList<T> ?? enumerable.ToList();
        return list.Count == 0 ? default : list[System.Random.Shared.Next(0, list.Count)];
    }
}