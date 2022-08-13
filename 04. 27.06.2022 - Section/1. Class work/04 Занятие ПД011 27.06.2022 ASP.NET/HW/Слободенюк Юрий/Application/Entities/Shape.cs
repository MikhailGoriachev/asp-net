namespace Application.Entities;


public interface IShape
{
    public string Type { get; }
    public double Perimeter { get; }
    public double Area { get; }
}


public class Triangle : IShape
{
    public double A { get; }
    public double B { get; }
    public double C { get; }

    public string Type => "Треугольник";
		
    public double Area => AreaImpl();

    public double Perimeter => A + B + C;

    
    public Triangle(double a, double b, double c) =>
        (A, B, C) = (a, b, c);
    

    private double AreaImpl()
    {
        double p = (A + B + C) / 2;

        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }
}


public class Rectangle : IShape
{
    public double A { get; }
    public double B { get; }

    public string Type => "Прямоугольник";
		
    public double Area => A * B;

    public double Perimeter => 2 * (A + B);

    
    public Rectangle(double a, double b) =>
        (A, B) = (a, b);
}


public class Square : IShape
{
    public double A { get; }

    public string Type => "Квадрат";
		
    public double Area => A * A;

    public double Perimeter => A * 4;

    
    public Square(double a) =>
        A = a;
}