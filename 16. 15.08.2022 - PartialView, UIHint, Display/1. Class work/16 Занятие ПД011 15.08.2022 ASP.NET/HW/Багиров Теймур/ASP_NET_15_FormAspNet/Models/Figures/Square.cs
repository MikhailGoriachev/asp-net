namespace ASP_NET_15_FormAspNet.Models.Figures;

public class Square : IFigure{
   
    // ид фигуры
    public int Id { get; set; }
    
    // название
    public string? Name => "Квадрат";

    // изображение
    public string? Image => "square.png";

    // сторона
    private double _side; 
    public double Side
    {
        get => _side;
        set => _side = value > 0d ? value
            : throw new InvalidDataException("Стороны должны быть больше 0");
    }

    // периметр
    public double Perimeter => Side * 4;

    // площадь
    public double Area => Side * Side;

    // словарь параметров и их значений
    public Dictionary<string, double> ParamAndValue => new() { ["a"] = Side }; 
}