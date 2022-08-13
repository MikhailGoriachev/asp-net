using System.Collections.Concurrent;
using System.Linq.Expressions;


namespace Application.Common;


public enum SortOrder
{
    Ascending,
    Descending
}


public static class Sorterer<T> where T : class
{
    private static readonly ConcurrentDictionary<string, Func<T, object>> _sorters = new();


    public static IOrderedEnumerable<T> OrderBy(IEnumerable<T> enumerable, string propertyName, SortOrder order)
    {
        var sorterer = _sorters.GetOrAdd(propertyName,
            prop => ToLambda(prop).Compile());

        return order switch
        {
            SortOrder.Ascending => enumerable.OrderBy(sorterer),
            SortOrder.Descending => enumerable.OrderByDescending(sorterer),
            _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
        };
    }


    private static Expression<Func<T, object>> ToLambda(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
}