namespace Homework.Infrastructure;

public static class Utilities
{
	public static Random Rand = new Random();

	public static double GenerateDouble(double from, double to) => from + Rand.NextDouble() * (to - from);
		
	public static int GenerateInt(int from, int to) => Rand.Next(from, to);
}