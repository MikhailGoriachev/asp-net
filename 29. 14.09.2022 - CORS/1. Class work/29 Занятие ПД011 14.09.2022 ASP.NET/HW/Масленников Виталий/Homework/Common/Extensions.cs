using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homework.Common;

public static class Extensions
{
    // Случайный элемент перечисляемой коллекции
    public static T? Random<T>(this IEnumerable<T> enumerable)
    {
        var list = enumerable as IList<T> ?? enumerable.ToList();
        return list.Count == 0 ? default : list[System.Random.Shared.Next(0, list.Count)];
    }

    // Сгенерировать вещественное число
    public static double NextDouble(this Random rand, double min, double max, int? precision = null)
    {
        var number = rand.NextDouble() * (max - min) + min;
        return precision is not null ? Math.Round(number, (int)precision) : number;
    }
}