namespace RoutesForms.Models.Task02;

// интерфейсный тип для фигур
public interface IFigure
{
    int Id       { get; }
    string Type  { get; }
    string Image { get; }

    double Area();
    double Perimeter();
} // IFigure
