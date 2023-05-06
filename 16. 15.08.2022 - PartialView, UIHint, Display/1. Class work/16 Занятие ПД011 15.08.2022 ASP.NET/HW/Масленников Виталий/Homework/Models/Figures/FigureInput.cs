
namespace Homework.Models.Figures;

public class FigureInput
{
    public string Name { get; set; } = null!;
    public int Id { get; set; }
    public double A { get; set; }
    public double? B { get; set; }
    public double? C { get; set; }

    public FigureInput()
    {
    }
    
    public FigureInput(IFigure figure)
    {
        Name = figure.Name;

        Id = figure.Id;
        
        A = figure.Parameters["a"];
        
        if(figure.Parameters.ContainsKey("b"))
            B = figure.Parameters["b"];
        
        if(figure.Parameters.ContainsKey("c"))
            C = figure.Parameters["c"];
    }

    public IFigure Figure() =>
        Name switch
        {
            "Квадрат" => new Square(Id, A),
            "Прямоугольник" => new Rectangle(Id, A, (int)B!),
            "Треугольник" => new Triangle(Id, A, (int)B!, (int)C!),
            _ => throw new ArgumentOutOfRangeException()
        };
    
    
}