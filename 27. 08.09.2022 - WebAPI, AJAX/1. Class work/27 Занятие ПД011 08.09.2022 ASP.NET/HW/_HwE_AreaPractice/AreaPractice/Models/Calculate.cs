namespace AreaPractice.Models;

// Модель для вычислений по задаче 1
public class Calculate
{
    // Исходные анные к вычислениям
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    // Вычисления по рисунку 1
    public Result Calc1() {

        double temp = 1 / (X * X + 4);
        double a = (1 + Y) * (X + Y * temp) / (Math.Exp(-X - 2) + temp);
        double b = (1 + Math.Cos(Y - 2)) / (X * X * X * X / 2 + Math.Pow(Math.Sin(Z), 2));

        return new Result(a, b);
    } // Calc1


    // Вычисления по рисунку 2
    public Result Calc2() {

        double a = (1 + Math.Pow(Math.Sin(X + Y), 2)) / (2 + Math.Abs(X - 2 * X / (1 + X * X * Y * Y))) + X;
        double b = Math.Pow(Math.Cos(Math.Atan(1 / Z)), 2);

        return new Result(a, b);
    } // Calc2
} // class Calculate

