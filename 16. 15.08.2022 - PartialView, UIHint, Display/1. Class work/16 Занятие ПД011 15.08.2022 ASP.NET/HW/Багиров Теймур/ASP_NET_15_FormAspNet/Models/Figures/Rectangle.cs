namespace ASP_NET_15_FormAspNet.Models.Figures;

public class Rectangle : IFigure {
    
    // ид фигуры
    public int Id { get; set; }
    
    // название
    public string? Name => "Прямоугольник";

    // изображение
    public string? Image => "rectangle.png";

    // сторона a
    private double _sideA;

    public double SideA
    {
        get => _sideA; 
        set => _sideA = value > 0d ? value
            : throw new InvalidDataException("Стороны должны быть больше 0");
    }

    // сторона b

    private double _sideB;

    public double SideB
    {
        get => _sideB;
        set => _sideB = value > 0d ? value
            : throw new InvalidDataException("Стороны должны быть больше 0");
    }

    // периметр
    public double Perimeter => (SideA + SideB) * 2;

    // площадь
    public double Area => SideA * SideB;

    // словарь параметров и их значений
    public Dictionary<string, double> ParamAndValue {
        get => new() { ["a"] = SideA, ["b"] = SideB };
    }
}