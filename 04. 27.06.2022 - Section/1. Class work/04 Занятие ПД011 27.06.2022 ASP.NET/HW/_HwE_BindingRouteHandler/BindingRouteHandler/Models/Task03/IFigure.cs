namespace BindingRouteHandler.Models.Task03
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
