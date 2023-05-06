namespace ASP_NET_15_FormAspNet.Models.Figures;

// Модель для привязки к форме (ид редактируемой фигуры, тип фигуры, сторона a, сторона b, сторона c)
public record FigureModel (
    int IdFigure, string Name, double SideA, double? SideB, double? SideC
);
