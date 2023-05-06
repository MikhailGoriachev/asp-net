namespace HomeWork.Models;

// Класс Выржение 2
public record Expression2(double X, double Y, double Z)
{
    // получить число A
    public double A => ((1 + Math.Pow(Math.Sin(X + Y), 2)) / (2 + Math.Abs(X - 2 * X / (1 + (X * X) * (Y * Y))))) + X;

    // получить число B
    public double B => Math.Pow(Math.Cos(Math.Atan(1 / Z)), 2);
}
