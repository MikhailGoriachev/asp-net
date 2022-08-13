namespace Application.Common;


public static class Extensions
{
    public static T Random<T>(this IList<T> collection) => collection[System.Random.Shared.Next(collection.Count)];
    
    public static double RealNextDouble(this Random rand, double min, double max) =>
        rand.NextDouble() * (max - min) + min;
}