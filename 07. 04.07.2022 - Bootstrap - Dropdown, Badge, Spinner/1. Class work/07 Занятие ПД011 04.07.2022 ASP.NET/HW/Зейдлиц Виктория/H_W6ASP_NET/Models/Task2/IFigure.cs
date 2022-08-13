namespace H_W6ASP_NET.Models.Task02
{
    // интерфейсный тип для фигур
    public interface IFigure
    {
        string Type { get; }
        string Image { get; }

        double Area();
        double Perimeter();
    } // IFigure
}
