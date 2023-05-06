using System.Linq.Expressions;
using System.Reflection;
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


    #region Сортировка по строковому имени свойства

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> src, string sort)
    {
        // извлекаем дерево выражений из запроса
        var queryExpression = src.Expression;
        // применяем сортировки
        queryExpression = queryExpression.OrderBy(sort);

        // уменьшаем дерево если есть лишние части
        if (queryExpression.CanReduce)
            queryExpression = queryExpression.Reduce();

        // создаем запрос
        src = src.Provider.CreateQuery<T>(queryExpression);

        return src;
    }


    // Формат строки: Property.NestedProperty asc, Property desc
    private static Expression OrderBy(this Expression source, string sort)
    {
        // получаем правила сортировок
        var orderBys = sort.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < orderBys.Length; i++)
        {
            // применяем каждое правило к дереву
            source = AddOrderBy(source, orderBys[i], i);
        }

        return source;
    }


    private static Expression AddOrderBy(Expression source, string orderBy, int index)
    {
        // отделяем названеи свойства от порядка сортировки
        var orderByParams = orderBy.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        // выясняем первый ли это вызов
        string orderByMethodName = index == 0 ? "OrderBy" : "ThenBy";
        // берем путь к свойству
        string parameterPath = orderByParams[0];
        // если выбрана сортировка по убыванию - изменяем название метода сортировки
        if (orderByParams.Length > 1 && orderByParams[1].Equals("desc", StringComparison.OrdinalIgnoreCase))
            orderByMethodName += "Descending";

        // берем тип модели у дерева
        var sourceType = source.Type.GetGenericArguments().First();
        // создаем параметр для будущей лямбды
        var parameterExpression = Expression.Parameter(sourceType, "p");
        // строим путь к свойству через параметр
        var orderByExpression = BuildPropertyPathExpression(parameterExpression, parameterPath);
        // рисуем тип метода OrderBy
        var orderByFuncType = typeof(Func<,>).MakeGenericType(sourceType, orderByExpression.Type);
        // делаем лямбду для доступа к члену типа p => Property.Nested.MoreNested
        var orderByLambda = Expression.Lambda(orderByFuncType, orderByExpression, parameterExpression);

        // возвращаем новое выражение, это вызов метода
        return Expression.Call(
            typeof(Queryable), // у типа Queryable
            orderByMethodName, // название метода
            new[] { sourceType, orderByExpression.Type }, // закрываем дженерики
            source, // даем исходное дерево
            orderByLambda // вставляем лямбду к свойству
        );
    }


    private static Expression BuildPropertyPathExpression(this Expression rootExpression, string propertyPath)
    {
        // отделяем названия вложенных свойств (Property.Nested.MoreNested)
        var parts = propertyPath.Split(new[] { '.' }, 2);
        var currentProperty = parts[0];

        const BindingFlags bindings = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
        // берем дескриптор текущего свойства
        var propertyDescription = rootExpression.Type.GetProperty(currentProperty, bindings);

        // если свойства нет - исключение
        if (propertyDescription == null)
            throw new($"Cannot find property {rootExpression.Type.Name}.{currentProperty}");

        // создаем выражение - доступ к члену типа
        var propExpr = Expression.Property(rootExpression, propertyDescription);

        // если остались еще свойства (большая вложенность)
        if (parts.Length > 1)
            // повторяем процедуры
            return BuildPropertyPathExpression(propExpr, parts[1]);

        return propExpr;
    }

    #endregion


    // Сгенерировать вещественное число
    public static double NextDouble(this Random rand, double min, double max, int? precision = null)
    {
        var number = rand.NextDouble() * (max - min) + min;
        return precision is not null ? Math.Round(number, (int)precision) : number;
    }

    public static string GetTimestamp(this DateTime dt) => DateTime.Now.ToString("yyyyMMddHHmmssfff");
}