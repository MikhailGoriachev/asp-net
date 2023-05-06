namespace RoutesForms.Models.Task02;

// представление фигуры в форме для редактирования
public class FigureDataModel
{
    // идентификатор фигуры
    public int Id { get; set; }

    // тип фигуры
    public string Type { get; set; } = String.Empty;

    // параметры фигуры 
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    // конструктор по умолчанию
    public FigureDataModel()
    {
        Id = 0;
        Type = "квадрат";
        A = B = C = 1;
    } // FigureDataModel


    // конструктор из интерфейсного типа
    public FigureDataModel(IFigure figure) {
        Id = figure.Id;
        Type = figure.Type;

        A = B = C = 0;

        // запись сто
        switch (figure.Type) {
            case "квадрат":
                A = ((Square)figure).A;
                break;

            case "прямоугольник":
                Rectangle rectangle = (Rectangle)figure;
                (A, B) = (rectangle.A, rectangle.B);
                break;

            // присваивание значений для треугольника
            default:
                Triangle triangle = (Triangle)figure;
                (A, B, C) = (triangle.A, triangle.B, triangle.C);
                break;
        } // switch
    } // FigureDataModel
}

