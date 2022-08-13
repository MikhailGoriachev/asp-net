namespace Homework.Common;

public static class Utils
{
    // Случайная дата в указанном промежутке
    public static DateTime RandomDate(DateTime from, DateTime? to = null) => 
        from.AddDays(Random.Shared.Next(((to ?? DateTime.Today) - from).Days));
}