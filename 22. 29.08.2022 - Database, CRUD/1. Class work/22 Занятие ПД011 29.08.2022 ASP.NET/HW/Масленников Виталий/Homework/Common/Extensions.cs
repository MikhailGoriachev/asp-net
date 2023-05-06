﻿using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Homework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

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
    public static IOrderedEnumerable<T> OrderBySort<T>(this IEnumerable<T> source, string propertyName ,string? sortRule = "asc") =>
        sortRule == "asc" ? source.OrderBy(ToLambda<T>(propertyName).Compile()) :
            source.OrderByDescending(ToLambda<T>(propertyName).Compile());
    public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string propertyName) =>
        source.OrderBy(ToLambda<T>(propertyName).Compile());

    public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source, string propertyName) =>
        source.OrderByDescending(ToLambda<T>(propertyName).Compile());

    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
    #endregion
    
    // Сгенерировать вещественное число
    public static double NextDouble(this Random rand, double min, double max, int? precision = null)
    {
        var number = rand.NextDouble() * (max - min) + min;
        return precision is not null ? Math.Round(number, (int)precision) : number;
    }

    public static string GetTimestamp(this DateTime dt) => DateTime.Now.ToString("yyyyMMddHHmmssfff");
    public static string NowDateTimeString(this DateTime dt) => $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";



    public static List<int> GetDiameters(this List<Planet> planets) =>
        planets.Select(p =>
        {
            var parse = int.TryParse(p.Diameter, out int diameter);
            return parse ? diameter : -1;
        }).Where(x => x > -1).ToList();
    
    public static List<int> GetOrbitals(this List<Planet> planets) =>
        planets.Select(p =>
        {
            var parse = int.TryParse(p.OrbitalPeriod, out int orbital);
            return parse ? orbital : -1;
        }).Where(x => x > -1).ToList();
    
    public static List<double> GetMasses(this List<Character> characters) =>
        characters.Select(c =>
        {
            var parse = double.TryParse(c.Mass.Replace(",", ""),
                NumberStyles.Any, CultureInfo.InvariantCulture, out double mass);
            return parse ? mass : -1;
        }).Where(x => x > -1).ToList();
}