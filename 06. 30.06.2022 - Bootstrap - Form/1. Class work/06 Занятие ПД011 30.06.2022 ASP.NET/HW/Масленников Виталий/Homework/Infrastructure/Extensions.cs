using System.Linq.Expressions;

namespace Homework.Infrastructure;

public static class Extensions
{
	#region Сортировка по свойству через передачу его имени в параметр

	public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName) => 
		source.OrderBy(ToLambda<T>(propertyName));

	public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName) => 
		source.OrderByDescending(ToLambda<T>(propertyName));

	private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
	{
		var parameter = Expression.Parameter(typeof(T));
		var property = Expression.Property(parameter, propertyName);
		var propAsObject = Expression.Convert(property, typeof(object));

		return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
	}

	#endregion


	public static double NextDouble(this Random rand, double min, double max) =>
		rand.NextDouble() * (max - min) + min;

	// Случайный элемент перечисляемой коллекции
	public static T? Random<T>(this IEnumerable<T> enumerable)
	{
		var list = enumerable as IList<T> ?? enumerable.ToList();
		return list.Count == 0 ? default(T) : list[System.Random.Shared.Next(0, list.Count)];
	}
}