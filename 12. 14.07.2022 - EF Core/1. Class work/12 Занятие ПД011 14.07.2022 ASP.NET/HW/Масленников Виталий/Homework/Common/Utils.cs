namespace Homework.Common;

public static class Utils
{
    public static DateTime RandomDate(DateTime from, DateTime to) => 
        from.AddDays(Random.Shared.Next((to - from).Days));
    
    public static string GetTimestamp() => DateTime.Now.ToString("yyyyMMddHHmmssfff");
}