namespace HomeWork.Models;

// Класс Выржение 1
public record Expression1(double X, double Y, double Z)
{
    // получить число A
    public double A => (1 + Y) * (X + Y / ((X * X) + 4)) / (Math.Pow(Math.E, -X - 2) + 1 / ((X * X * X) + 4));

    // получить число B
    public double B => (1 + Math.Cos(Y - 2)) / (Math.Pow(X, 4) / 2 + Math.Pow(Math.Sin(Z), 2));
}
