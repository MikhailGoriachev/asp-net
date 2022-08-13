using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Homework.Common
{
	public static class Extensions
	{
		#region Сортировка по строковому имени свойства
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
		public static double NextDouble(this Random rand, double min, double max) =>
			rand.NextDouble() * (max - min) + min;

		// Случайный элемент перечисляемой коллекции
		public static T? Random<T>(this IEnumerable<T> enumerable)
		{
			var list = enumerable as IList<T> ?? enumerable.ToList();
			return list.Count == 0 ? default : list[System.Random.Shared.Next(0, list.Count)];
		}
		
		public static string GetTimestamp(this DateTime dt) => DateTime.Now.ToString("yyyyMMddHHmmssfff");
	}
}